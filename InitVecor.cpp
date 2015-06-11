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
	cout<<"������vector"<<endl<<"vector����:"; 
	vector <int> vec;
	for_each(vec.begin(),vec.end(),print<int>());
	cout<<endl<<endl;
}
{ 
	cout<<"����8��Ԫ�ص�vector"<<endl<<"vector����:"; 
	vector <int> vec(8);
	for_each(vec.begin(),vec.end(),print<int>());
	cout<<endl<<endl;
} 
{ 
	cout<<"����Ĭ��ֵΪ1024��8��Ԫ�ص�vector"<<endl<<"vector����:"; 
	vector <int> vec(8,1024);
	for_each(vec.begin(),vec.end(),print<int>());
	cout<<endl<<endl;
} 
{ 
	cout<<"������ǰ4��Ԫ�ش�����vector"<<endl;
	int ia[]={0,1,2,3,4,5,6,7,8,9};
	cout<<"��������:";
	for(int i=0;i<sizeof(ia)/sizeof(int);i++)
		cout<<ia[i]<<" ";
	vector <int> vec(ia,ia+4);
	cout<<endl<<"vector����";
	for_each(vec.begin(),vec.end(),print<int>());
	cout<<endl<<endl;
} 
{ 
	cout<<"�����е�vector������vector"<<endl;
	vector<int> oldvec;
	for(int i=0;i<10;i++)
		oldvec.push_back(i); 
	cout<<"ԭ������:";
	for_each(oldvec.begin(),oldvec.end(),print<int>());
	vector <int> vec(oldvec);
	cout<<endl<<"vector����";
	for_each(vec.begin(),vec.end(),print<int>());
	cout<<endl<<endl;
} 
	return 0;
}
