#include <iostream>
#include <time.h>
using namespace std; 
int main(int argc, char *argv[])
{
	cout<<"�����ʼ��"<<endl; 
	static const int Count = 500000;//���鳤�� 
	int num[Count];//���� 
	clock_t startTime,stopTime;//��ֹʱ�� 

	//��ʼ���������� 
	for(int i=0 ;i<Count;i++)
	{
		num[i] = i;
	}
	cout<<"��ʼ����ϣ�"<<endl;


	//ʹ��forѭ��Count��ʽ���� 
	startTime = clock();
	for(int ccc = 0; ccc<10000; ccc++)
	{
		for(int i=0 ;i< Count-1;i++) 
		{
			num[i]++;
		}
	}
	stopTime = clock();
	cout<<"forѭ��Count��ʽ\t\t"<<stopTime-startTime<<"��λ"<<endl;


	//ʹ��forѭ��Count��ʽ���� 
	startTime = clock();
	for(int ccc = 0; ccc<10000; ccc++)
	{
		int *start =&num[0],*stop=&num[Count];	
		while(start!=stop)
			++(*(start++));
	}
	stopTime = clock();
	cout<<"whileѭ�����ұշ�ʽ\t\t"<<stopTime-startTime<<"��λ"<<endl;
}
