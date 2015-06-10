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
	int ia[]={1,2,3,4,5,6};
	vector<int> iv(ia,ia+sizeof(ia)/sizeof(int));
	for_each(iv.begin(),iv.end(),print<int>());
	return 0;
}
