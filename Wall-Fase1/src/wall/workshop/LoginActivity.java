package wall.workshop;

import java.util.HashMap;
import java.util.Map;

import org.json.JSONException;
import org.json.JSONObject;

import wall.workshop.images.ImageCacheManager;
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
	Button buttonLogin;
	EditText usernameInput, passwordInput;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);

		setContentView(R.layout.activity_login);

		userImage = (NetworkImageView) this.findViewById(R.id.userImage);
		buttonLogin = (Button) this.findViewById(R.id.buttonLogin);
		buttonLogin.setOnClickListener(this);
		usernameInput = (EditText) this.findViewById(R.id.usernameInput);
		passwordInput = (EditText) this.findViewById(R.id.passwordInput);

		userImage.setImageUrl(
				prefs.getString("avatar", MainApplication.defaultAvatar),
				ImageCacheManager.getInstance().getImageLoader());
	}

	private void login(final String username, final String password) {

		Map<String, String> map = new HashMap<String, String>();
		map.put("username", username);
		map.put("password", password);

		Response.Listener<String> response = new Response.Listener<String>() {

			@Override
			public void onResponse(String response) {

				// TODO:Login risposta
				try {
					Log.d("Wall", response);
					// convert response to JSON (see notes/main activity)
					JSONObject json = new JSONObject(response);
					if (json.getBoolean("error") == true)
						showToast("Errore");
					else {
						salvaLogin(username, password, json.optString("avatar",
								MainApplication.defaultAvatar));
						showToast("OK");
					}

				} catch (JSONException e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
			}
		};
		this.callVolley(MainApplication.login, response, map,
				LoginActivity.this);

	}

	private void salvaLogin(String username, String password, String avatar) {
		SharedPreferences.Editor editor = prefs.edit();
		editor.putString("username", username);
		editor.putString("password", password);
		editor.putString("avatar", avatar);
		editor.commit();
	}

	@Override
	public void onClick(View v) {
		// TODO:Login button
		login(usernameInput.getText().toString(), passwordInput.getText()
				.toString());
	}

}
