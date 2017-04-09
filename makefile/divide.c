#include <stdio.h>
#include "math.h"

int divide(int x, int y)
{
	if(y==0)
		{
			printf("y can't be zero\n");
			return 0;
		}
	return x/y;
}
