package wall.workshop;
 

import wall.workshop.images.ImageCacheManager;

import com.android.volley.RequestQueue;
import com.android.volley.toolbox.Volley;

import android.app.Application;
import android.graphics.Bitmap.CompressFormat;
 
public class MainApplication extends Application {

	private static RequestQueue mRequestQueue;
	private static int DISK_IMAGECACHE_SIZE = 1024*1024*10;
	private static CompressFormat DISK_IMAGECACHE_COMPRESS_FORMAT = CompressFormat.JPEG;
	private static int DISK_IMAGECACHE_QUALITY = 100;   
	
	public static final String login="http://54.243.204.111/wall/login.php";
	public static final String getWall="http://54.243.204.111/wall/getWall.php";
	public static final String postWall="http://54.243.204.111/wall/postWall.php";
	public static final String defaultAvatar="http://54.243.204.111/avatars/avatar_50.jpg";
	
	@Override
	public void onCreate() {
		super.onCreate();

		init();
	}
 
	private void init() {
		mRequestQueue = Volley.newRequestQueue(this.getApplicationContext()); 
		createImageCache();
	}
	
	/**
	 * Create the image cache. 
	 */
	private void createImageCache(){
		ImageCacheManager.getInstance().init(this.getApplicationContext(),
				this.getPackageCodePath()
				, DISK_IMAGECACHE_SIZE
				, DISK_IMAGECACHE_COMPRESS_FORMAT
				, DISK_IMAGECACHE_QUALITY);
	}
	
	public static RequestQueue getRequestQueue() {
	    if (mRequestQueue != null) {
	        return mRequestQueue;
	    } else {
	        throw new IllegalStateException("Not initialized");
	    }
	}

}