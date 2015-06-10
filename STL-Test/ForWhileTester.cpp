#include <iostream>
#include <time.h>
using namespace std; 
int main(int argc, char *argv[])
{
	cout<<"程序初始化"<<endl; 
	static const int Count = 500000;//数组长度 
	int num[Count];//数组 
	clock_t startTime,stopTime;//起止时间 

	//初始化数组内容 
	for(int i=0 ;i<Count;i++)
	{
		num[i] = i;
	}
	cout<<"初始化完毕："<<endl;


	//使用for循环Count方式计算 
	startTime = clock();
	for(int ccc = 0; ccc<10000; ccc++)
	{
		for(int i=0 ;i< Count-1;i++) 
		{
			num[i]++;
		}
	}
	stopTime = clock();
	cout<<"for循环Count方式\t\t"<<stopTime-startTime<<"单位"<<endl;


	//使用for循环Count方式计算 
	startTime = clock();
	for(int ccc = 0; ccc<10000; ccc++)
	{
		int *start =&num[0],*stop=&num[Count];	
		while(start!=stop)
			++(*(start++));
	}
	stopTime = clock();
	cout<<"while循环左开右闭方式\t\t"<<stopTime-startTime<<"单位"<<endl;
}
