# DataObtainingAndHandling

ȥ��ģ��ӿ�word���֣�
Public string denoiseWord(string path);
Require:�����ļ���ַpath
Effect:word�ļ����룬���ؽ������ı�
Modified:��

��ȡ�ؼ���ģ�飺
Public string getKey(string doc);
Require:������Ҫ��ȡ�ؼ��ʵ��ı�
Effect:��ȡ�ؼ��ʣ����ҷ���ؼ��ʣ����ı�����ȡ���Ĺؼ��ʺͷ�����д���ַ�������
Modified:��

Private string segmentWord(string doc);
Require:���뽵�����ı�
Effect:���ı��ִʣ����ҽ����ݷִʺ�Ľ����ȡ�ؼ���
Modified:��

Private string translate(string key);
Require:������ȡ���Ĺؼ���
Effect:����ؼ��ʣ����ؼ��ʺͷ������ı�һ�𷵻�
Modified:��

