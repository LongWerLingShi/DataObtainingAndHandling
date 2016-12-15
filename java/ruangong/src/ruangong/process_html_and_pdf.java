package ruangong;

import com.sun.media.jfxmedia.track.Track.Encoding;

public class process_html_and_pdf {
	public void process(String filepath,String url,Encoding e){
		e.toString();
		process(filepath,url,1);
	}
	// to do:
	// define each encoding to i 
	public native void process(String filepath,String url,int e);
	static {
		System.loadLibrary("ConsoleApplication5");
	}
	

}
