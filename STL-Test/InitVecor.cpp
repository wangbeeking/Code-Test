#include <iostream>
#include <vector>
using namespace std;

template <typename T>
class print
{
public:
	void operator()(const T& elem)
	{
		cout<<elem<<" "; 
	}
};

int main()
{
{ 
	cout<<"创建空vector"<<endl<<"vector数据:"; 
	vector <int> vec;
	for_each(vec.begin(),vec.end(),print<int>());
	cout<<endl<<endl;
}
{ 
	cout<<"创建8个元素的vector"<<endl<<"vector数据:"; 
	vector <int> vec(8);
	for_each(vec.begin(),vec.end(),print<int>());
	cout<<endl<<endl;
} 
{ 
	cout<<"创建默认值为1024的8个元素的vector"<<endl<<"vector数据:"; 
	vector <int> vec(8,1024);
	for_each(vec.begin(),vec.end(),print<int>());
	cout<<endl<<endl;
} 
{ 
	cout<<"以数组前4个元素创建的vector"<<endl;
	int ia[]={0,1,2,3,4,5,6,7,8,9};
	cout<<"数组数据:";
	for(int i=0;i<sizeof(ia)/sizeof(int);i++)
		cout<<ia[i]<<" ";
	vector <int> vec(ia,ia+4);
	cout<<endl<<"vector数据";
	for_each(vec.begin(),vec.end(),print<int>());
	cout<<endl<<endl;
} 
{ 
	cout<<"以已有的vector创建的vector"<<endl;
	vector<int> oldvec;
	for(int i=0;i<10;i++)
		oldvec.push_back(i); 
	cout<<"原有数据:";
	for_each(oldvec.begin(),oldvec.end(),print<int>());
	vector <int> vec(oldvec);
	cout<<endl<<"vector数据";
	for_each(vec.begin(),vec.end(),print<int>());
	cout<<endl<<endl;
} 
	return 0;
}
