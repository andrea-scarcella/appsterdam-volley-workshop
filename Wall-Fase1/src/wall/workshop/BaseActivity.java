package wall.workshop;

import java.util.HashMap;
import java.util.Map;

import org.json.JSONException;
import org.json.JSONObject;

import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.Request.Method;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;

import android.os.Bundle;
import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.SharedPreferences;
import android.util.Log;
import android.view.Menu;
import android.widget.Toast;

public class BaseActivity extends Activity {


	SharedPreferences prefs;
	ProgressDialog dialog;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        


    	prefs =getSharedPreferences(
    		      "wall", Context.MODE_PRIVATE);
    }
 
    
    
    public void callVolley(String url, Response.Listener<String> response, final Map<String, String> params, Context context){
    	
    	
     showWait(context);

   	 StringRequest login = new StringRequest(Method.POST,
   			  url, response, new Response.ErrorListener() {

   	  		@Override
   	  		public void onErrorResponse(VolleyError error) {
   	  		}
   	  	}) {
   		
   		protected Map<String, String> getParams() throws com.android.volley.AuthFailureError { 
   			     
   			return params;
   		};
   		};
   		 

   		MainApplication.getRequestQueue().add(login);
    }
    
    
    public void showToast(String message){
    	Toast.makeText(getApplicationContext(), message, Toast.LENGTH_SHORT).show();
    }
    
    public void showWait(Context context){
    	dialog = new ProgressDialog(context);

    	dialog.setTitle("Please wait");
    	dialog.setMessage("Philosoraptor is immersed in metaphysical inquiries");
    	dialog.setCancelable(true);
    	dialog.setIndeterminate(true);
    	dialog.show();
    } 
    public void dismissWait(){
    	if (dialog!=null) {
    		dialog.dismiss(); 
		}
    }
    @Override 
    protected void onDestroy() {
    	dismissWait();
    	super.onDestroy();
    }
}
