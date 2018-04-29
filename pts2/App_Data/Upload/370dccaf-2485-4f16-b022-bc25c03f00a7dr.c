#include<stdio.h>
#include<string.h>
#define ROW 3
#define COL 6
void spiralprint(int m,int n,char a[ROW][COL]);
main(){
	char a[ROW][COL]={"abcdef","ghijkl","mnopqr"};
	spiralprint(ROW,COL,a);
}
void spiralprint(int m,int n,char a[ROW][COL]){
	int i,k=0,l=0;
	while(k<m && l<n){
		for(i=l;i<n;++i){
			printf("%c",a[k][i]);
		}
		k++;
		
		for(i=k;i<m;++i){
			printf("%c",a[i][n-1]);
		}
		n--;
	
	if(k<m){
		for(i=n-1;i>=l;--i){
			printf("%c",a[m-1][i]);
		}
		m--;
	}


if(l<n){
	for(i=m-1;i>=k;--i){
		printf("%c",a[i][l]);
	}
	l++;
}}}
