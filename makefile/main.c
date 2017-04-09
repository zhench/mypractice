#include <stdio.h>
#include "math.h"

int main()
{
	int sum=0;
	sum = add(4,3);
	printf("sum: %d\n", sum);
	
	int result =0;
	result=divide(4,0);
	printf("result:%d\n",result);
	return 0;

}
