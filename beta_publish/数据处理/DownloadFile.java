public class DownloadFile {
	public native void getFromNetWorkConnection(String remotepath, String localpath, String username, String password, String remotefilename, String localfilename);
	public native void DeleteFileFromLocal(String localpath, String localfilename);
	
	static {  
        System.loadLibrary("Download_dll"); 
    }
	
	public static void main(String[] args) {  
        DownloadFile down = new DownloadFile();
        //down.DeleteFileFromLocal("G:\\", "text.txt");
        down.getFromNetWorkConnection("\\\\10.2.28.78\\XueBaResources", "E:\\AppServ\\www\\datahandler2\\console", "Crawler", "Ase12345678", "2000004.html", "2000004.html");
        System.out.println(System.getProperty("java.library.path"));
    } 
	
}
