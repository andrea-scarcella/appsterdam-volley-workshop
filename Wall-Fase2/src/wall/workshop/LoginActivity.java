package wall.workshop;
 

import java.util.HashMap;
import java.util.Map;

import org.json.JSONException;
import org.json.JSONObject;

import wall.workshop.images.ImageCacheManager;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.EditText;

import com.android.volley.Response;
import com.android.volley.toolbox.NetworkImageView;

public class LoginActivity extends BaseActivity implements OnClickListener {
	NetworkImageView userImage;
	Button buttonLogin;;
	EditText usernameInput,passwordInput;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        
 
        setContentView(R.layout.activity_login);
        
        
        userImage = (NetworkImageView) this.findViewById(R.id.userImage);
        buttonLogin = (Button) this.findViewById(R.id.buttonLogin);        
        buttonLogin.setOnClickListener(this);
        usernameInput = (EditText) this.findViewById(R.id.usernameInput);
        passwordInput = (EditText) this.findViewById(R.id.passwordInput);
        
        userImage.setImageUrl(prefs.getString("avatar",MainApplication.defaultAvatar), ImageCacheManager.getInstance().getImageLoader());
		
        loginCheck();
        
        
      
        
    }
    
private void loginCheck(){
	String username = prefs.getString("username", null);
	String password = prefs.getString("password", null);
	
	if(username!=null && password!=null){
		
		usernameInput.setText(username);
		passwordInput.setText(password);
		login(username,password);
	} 
}


private void login(final String username,final String password){

	Map<String, String> params = new HashMap<String,String>();
	params.put("username", username); 
	params.put("password", password);
	

	  Response.Listener<String> response =  new Response.Listener<String>() {

	  		@Override
	  		public void onResponse(String response) { 

	  			 try {
					JSONObject json = new JSONObject(response);
					if(json.getBoolean("login")==true){ 
						 
							showToast(username + " loggato!");
							salvaLogin(username,password,json.optString("avatar", MainApplication.defaultAvatar));
							apriWall(username,json.optString("avatar", MainApplication.defaultAvatar));
							
					}else showToast("Username o password errati!");
					
				} catch (JSONException e) {
					showToast("Errore!");
					e.printStackTrace();
				}
	  			 
	  		}
	  	}; 
	
	callVolley(MainApplication.login,response,params,LoginActivity.this);

		    

 
}



	@Override
	public void onClick(View v) {
		if(usernameInput.getText().length()>3 && passwordInput.getText().length()>3)
			login(usernameInput.getText().toString(),passwordInput.getText().toString());
		else 
			showToast("Username o password mancanti!");
		
	}
	
	private void salvaLogin(String username, String password,String avatar){

		SharedPreferences.Editor editor = prefs.edit();
		 editor.putString("username", username);
		 editor.putString("password", password); 
		 editor.putString("avatar", avatar); 
		 editor.commit();
	}
	
	private void apriWall(String username,String avatar){
		finish();
		
		Intent intentWall = new Intent(LoginActivity.this, WallActivity.class);
		 
		intentWall.putExtra("username", username);
		intentWall.putExtra("avatar", avatar);  
		 
		startActivity(intentWall);
	}
    
}
