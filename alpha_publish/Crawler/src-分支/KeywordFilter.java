import org.htmlparser.beans.StringBean;
/**
* description: ���˷��Ϲ�����������ҳ
* note: �����ٶ��ȶ�
* modificationDate: 2014-11-29
*/ 
public class KeywordFilter {
	private String[] keywords;
	public KeywordFilter(String[] keywords)
	{
		this.keywords = keywords;
	}
	/** 
     * �����ṩ��URL����ȡ��URL��Ӧ��ҳ�Ĵ��ı���Ϣ 
     * @return ��Ӧ��ҳ�Ĵ��ı���Ϣ Striing
     * @param ��ַ����url 
     * @throws ParserException 
     */  
    public static String getText(String url){  
        StringBean sb = new StringBean();  
          
        //���ò���Ҫ�õ�ҳ����������������Ϣ  
        sb.setLinks(false);  
        //���ý�����Ͽո�������ո������  
        sb.setReplaceNonBreakingSpaces(true);  
        //���ý�һ���пո���һ����һ�ո�������  
        sb.setCollapse(true);  
        //����Ҫ������URL  
        sb.setURL(url);  
        //���ؽ��������ҳ���ı���Ϣ  
        return sb.getStrings();  
    } 
    
    /** 
     * ����ָ��URL�Ƿ���Ϲ�������
     * @return �Ƿ���Ϲ�������(int)
     * @param ��ַ����url 
     * @throws  
     */
	public boolean accept(String url)//	boolean usual
	{
		if(keywords[0].equals("")){
			return true;
		}
		else{
			for(int i = 0; i < keywords.length; i++){
				if(url.contains(keywords[i]) || getText(url).contains(keywords[i])){
					return true;
				}
			}
			return false;
		}
	}
}
