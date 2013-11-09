package wall.workshop;
 

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;

import org.json.JSONException;
import org.json.JSONObject;

import wall.workshop.images.ImageCacheManager;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.EditText;
import android.widget.ImageButton;
import android.widget.ListView;

import com.android.volley.Response;
import com.android.volley.toolbox.NetworkImageView;

public class WallActivity extends BaseActivity implements OnClickListener {
	NetworkImageView userImage;
	ImageButton buttonSend;
	EditText text;
	ListView listViewPosts;
	WallAdapter wallAdapter;
	
	String username,avatar;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        
 
        setContentView(R.layout.activity_main);
        username = getIntent().getStringExtra("username");
        avatar = getIntent().getStringExtra("avatar");
        
        userImage = (NetworkImageView) this.findViewById(R.id.userImage);
        buttonSend = (ImageButton) this.findViewById(R.id.buttonSend);        
        buttonSend.setOnClickListener(this);
        text = (EditText) this.findViewById(R.id.text); 
        listViewPosts = (ListView) this.findViewById(R.id.listViewPosts); 
        wallAdapter = new WallAdapter(this,R.layout.list_item, new ArrayList<WallModel>()); 
        
        listViewPosts.setAdapter(wallAdapter);

        userImage.setImageUrl(avatar, ImageCacheManager.getInstance().getImageLoader());
        
    }
     

	@Override
	public void onClick(View v) {
 
		if(text.getText().length()>3) postWall();
	}
	
	

private void postWall(){ 

//TODO: impostare parametri per invio messaggio e inviare request	
 
	 
}
 
@Override
public boolean onCreateOptionsMenu(Menu menu)
{
	 MenuInflater inflater = getMenuInflater();
	    inflater.inflate(R.menu.wall, menu);
	    return true;
}
 

@Override
public boolean onOptionsItemSelected(MenuItem item)
{
     
    switch (item.getItemId())
    {
    case R.id.action_refresh:
 
    	wallAdapter.refresh();
        return true;

    
    default:
        return super.onOptionsItemSelected(item);
    }
}    

    
}
