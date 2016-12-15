// ConsoleApplication5.cpp : 定义 DLL 应用程序的导出函数。
//

#include "stdafx.h"
#include "string.h"
#include "jni.h" 
#include "jni_md.h"
#include "ruangong_process_html_and_pdf.h"

#include <malloc.h>
#include <stdlib.h>
#include <vcclr.h> 
#using "ClassLibrary1.dll"
using namespace ClassLibrary1;

#using "mscorlib.dll"
#using "System.dll"
using namespace System;
// char* To jstring
jstring stringTojstring(JNIEnv* env, const char* pat)
{
	jclass strClass = env->FindClass("Ljava/lang/String;");
	jmethodID ctorID = env->GetMethodID(strClass, "<init>", "([BLjava/lang/String;)V");
	jbyteArray bytes = env->NewByteArray(strlen(pat));
	env->SetByteArrayRegion(bytes, 0, strlen(pat), (jbyte*)pat);
	jstring encoding = env->NewStringUTF("utf-8");
	return (jstring)env->NewObject(strClass, ctorID, bytes, encoding);
}
// jstring To char*
char* jstringTostring(JNIEnv* env, jstring jstr)
{
	char* rtn = NULL;
	jclass clsstring = env->FindClass("java/lang/String");
	jstring strencode = env->NewStringUTF("utf-8");
	jmethodID mid = env->GetMethodID(clsstring, "getBytes", "(Ljava/lang/String;)[B");
	jbyteArray barr = (jbyteArray)env->CallObjectMethod(jstr, mid, strencode);
	jsize alen = env->GetArrayLength(barr);
	jbyte* ba = env->GetByteArrayElements(barr, JNI_FALSE);
	if (alen > 0)
	{
		rtn = (char*)malloc(alen + 1);
		memcpy(rtn, ba, alen);
		rtn[alen] = 0;
	}
	env->ReleaseByteArrayElements(barr, ba, 0);
	return rtn;
}
// jstring To String
String^ jstringToStr(JNIEnv* env, jstring jstr)
{
	char* str = jstringTostring(env, jstr);
	String^ value = gcnew String(str);
	free(str);
	return value;
}

// String To jstring
jstring strTojstring(JNIEnv* env, String^ rtn)
{
	pin_ptr<const wchar_t> wch = PtrToStringChars(rtn);
	size_t convertedChars = 0;
	size_t sizeInBytes = ((rtn->Length + 1) * 2);
	char *ch = (char *)malloc(sizeInBytes);
	errno_t err = wcstombs_s(&convertedChars,
		ch, sizeInBytes,
		wch, sizeInBytes);
	jstring js = stringTojstring(env, ch);
	free(ch);
	return js;
}


JNIEXPORT void JNICALL Java_ruangong_process_1html_1and_1pdf_process
(JNIEnv *env, jobject, jstring filepath, jstring url, jint encoding)
{
	// to do  define encoding 
	process ^o = gcnew process(jstringToStr(env,filepath), jstringToStr(env,url), encoding);
	return ;
}