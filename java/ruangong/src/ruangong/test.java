package ruangong;

import java.util.Scanner;

public class test {

	public static void main(String[] args) {
		// TODO Auto-generated method stub

		Scanner input=new Scanner(System.in);
		String path=input.nextLine();
		System.out.println(path);
		String url="http://zhidao.baidu.com/question/137458943246240365.html?fr=iks&word=string+%BA%BA%D7%D6+jstring+c%23&ie=gbk";
		Process_html_pdf proc=new Process_html_pdf(path,url,1);
		proc.run();
		System.out.println(proc.getTitle());
		System.out.println(proc.getQuestion());
		System.out.println(proc.getAnswer());
		System.out.println(proc.getAuthor());
		System.out.println(proc.getDate());
	}

}
