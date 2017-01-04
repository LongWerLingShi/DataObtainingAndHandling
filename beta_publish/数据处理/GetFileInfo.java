import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.IOException;
//import java.util.ArrayList;
import org.json.JSONException;
import org.json.JSONObject;
//import org.json.JSONString;
//import org.json.JSONArray;

public class GetFileInfo {
	private boolean allowOpen;
	private boolean allowClear;
	public GetFileInfo()
	{
		allowOpen =false;
		allowClear = false;
	}
	public BufferedReader getBuffer(String path)
	{
		BufferedReader br;
		while(true)
		{
			try
			{
				 br = new BufferedReader(new FileReader(path));
				 break;
			}
			catch (IOException e) {  
				try {
					BufferedWriter bw = new BufferedWriter(new FileWriter(path));
					String wString = "{\n\t\"allowOpen\":\"true\"\n}";
					bw.write(wString);
					bw.close();
				} catch (IOException e1) {}
		    }
		}
		return br;
	}
	public boolean getInfo2(String path)
	{
		BufferedReader br = getBuffer(path);
		try {
			String s= null;
			String str = "";
			while ((s = br.readLine()) != null) 
			{  
			   str += s;      
			}
			br.close();  
			try {  
               JSONObject dataJson = new JSONObject(str); 
               String boolAllocOpen = dataJson.getString("allowClear");
               allowClear = (boolAllocOpen.equals("true"))?true:false;
           } catch (JSONException e) {}
		} catch (IOException e1) {}
		return allowClear;
	}
	public boolean getInfo(String path)
	{
		BufferedReader br = getBuffer(path);
		try {
			String s= null;
			String str = "";
			while ((s = br.readLine()) != null) 
			{  
			   str += s;      
			}
			br.close();  
			try {  
               JSONObject dataJson = new JSONObject(str); 
               String boolAllocOpen = dataJson.getString("allowOpen");
               allowOpen = (boolAllocOpen.equals("true"))?true:false;
           } catch (JSONException e) {}
		} catch (IOException e1) {}
		return allowOpen;
	}
	public void setInfo(String path,String string)
	{
		while(true)
		{
			try {
				BufferedWriter bw = new BufferedWriter(new FileWriter(path));
				bw.write(string);
				bw.close();
				break;
			} catch (IOException e1) {}
		}
	}
	/*
	try {  
		
        
        BufferedWriter bw = new BufferedWriter(new FileWriter(  
                "src/json/HK_new.json"));// 输出新的json文件  
        String s = null, ws = null;  
        while ((s = br.readLine()) != null) {  
            // System.out.println(s);  
            try {  
                JSONObject dataJson = new JSONObject(s);// 创建一个包含原始json串的json对象  
                JSONArray features = dataJson.getJSONArray("features");// 找到features的json数组  
                for (int i = 0; i < features.length(); i++) {  
                    JSONObject info = features.getJSONObject(i);// 获取features数组的第i个json对象  
                    JSONObject properties = info.getJSONObject("properties");// 找到properties的json对象  
                    String name = properties.getString("name");// 读取properties对象里的name字段值  
                    System.out.println(name);  
                    properties.put("NAMEID", list.get(i));// 添加NAMEID字段  
                    // properties.append("name", list.get(i));  
                    System.out.println(properties.getString("NAMEID"));  
                    properties.remove("ISO");// 删除ISO字段  
                }  
                ws = dataJson.toString();  
                System.out.println(ws);  
            } catch (JSONException e) {  
                // TODO Auto-generated catch block  
                e.printStackTrace();  
            }  
        }  

        bw.write(ws);  
        // bw.newLine();  

        bw.flush();  
        br.close();  
        bw.close();  

    } catch (IOException e) {  
        // TODO Auto-generated catch block  
        e.printStackTrace();  
    }  
}*/
}
