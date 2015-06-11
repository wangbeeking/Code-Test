#include <iostream>
using namespace std;

class INT
{
public:
	INT():m_i(3){};
	friend void Print(const INT& obj);//ÉùÃ÷ÓÑÔªº¯Êý
private:
	int m_i;
};
void Print(const INT& obj)
{
	cout<<obj.m_i<<endl;
}
int main()
{
	INT in;
	Print(in);

	system("pause");
	return 0;
}