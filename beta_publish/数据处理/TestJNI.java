public class TestJNI {  
	  
	public native void denoiseWord(String path,int trd);  
    public native void cutwords(int trd, String analyzer);
    public native void key(String Path,int trd);
    public native void translate(int trd);
    
  
    static {  
        System.loadLibrary("c++dll"); 
        //System.loadLibrary("Lucene.Net");
    }  
    public static void main(String[] args) {  
    	String URL = "F:\\123.docx";
    	int threadNo = 1;
    	TestJNI t = new TestJNI(); 
    	t.denoiseWord(URL,threadNo);
        t.cutwords(1, "Lucene.China.ChineseAnalyzer");
        t.key("CorpusWordlist.xls", threadNo);
        t.translate(threadNo);
        System.out.println("end");
        System.out.println("end");
    }   
    public static void processWord(String URL,int threadNo) {  
    	//System.out.println(System.getProperty("java.library.path"));
    	//System.out.println(URL);
    	TestJNI t = new TestJNI(); 
    	t.denoiseWord(URL,threadNo);
    	//System.out.println(5555555);
        t.cutwords(threadNo, "Lucene.China.ChineseAnalyzer");
        //System.out.println(1266662);
        t.key("CorpusWordlist.xls", threadNo);
        //System.out.println(414213413);
        t.translate(threadNo);
        //System.out.println("end");
    }
}  

