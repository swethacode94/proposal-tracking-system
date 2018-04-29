#include<stdlib.h>
#include<stdio.h>
#include<string.h>
struct node{
	int info;
	struct node *link;
};
struct node *createlist(struct node *start);
struct node *addatbeg(struct node *start,int data);
struct node *addatend(struct node *start,int data);
struct node *addatpos(struct node *start,int data,int pos);
struct node *addafter(struct node *start,int data,int item);
struct node *addbefore(struct node *start,int data,int item);
struct node *reverse(struct node *start);
struct node *deleteduplicates(struct node *start);
struct node *deletefirstnode(struct node *start);
struct node *deletelastnode(struct node *start);
struct node *deleteitem(struct node *start,int data);
struct node *deletepos(struct node *start,int pos);
struct node *deletefull(struct node *start);
void search(struct node *start,int item);
void display(struct node *start);
void count(struct node *start);
main(){
	int choice ,data,item,pos;
	struct node *start=NULL;
	while(1){
		printf("1. create list\n");
		printf("2. add at beg\n");
		printf("3. add at end\n");
		printf("4. add at pos\n");
		printf("5. add after\n");
		printf("6. add before\n");
		printf("7. reverse\n");
		printf("8. delete duplicates\n");
		printf("9. delete frist node\n");
		printf("10. delete last node\n");
		printf("11. delete item\n");
		printf("12. delete pos\n");
		printf("13. delete full\n");
		printf("14. search\n");
		printf("15. display\n");
		printf("16. count\n");
		printf("17. quit\n");
		
		printf("enter you choice");
		scanf("%d",&choice);
		switch(choice){
			case 1:
				start = createlist(start);
				break;
				case 2:
					printf("enter the data");
					scanf("%d",&data);
				start=addatbeg(start,data);
				break;
				case 3:
					printf("enter the data");
					scanf("%d",&data);
				start=addatend(start,data);
				break;
				case 4:
					printf("enter the data");
					scanf("%d",&data);
					printf("enter the position at you want to insert");
					scanf("%d",&pos);
					start=addatpos(start,data,pos);
					break;
					case 5:
							printf("enter the data");
					scanf("%d",&data);
					printf("enter the data after which you want to insert");
					scanf("%d",&item);
					start=addafter(start,data,item);
					case 6:
						printf("enter the data");
					scanf("%d",&data);
					printf("enter the data befor which you want to insert");
					scanf("%d",&item);
					start=addbefore(start,data,item);
					case 7:
						start=reverse(start);
						break;
						case 8:
						start=deleteduplicates(start);
						break;
						case 9:
						start=deletefirstnode(start);
						break;
						case 10:
							start=deletelastnode(start);
							case 11:
								printf("enter data to be delete");
								scanf("%d",&data);
								start=deleteitem(start,data);
								break;
								case 12:
									printf("enter pos");
								scanf("%d",&pos);
								start=deletepos(start,pos);
								break;
								case 13:
									start=deletefull(start);
									break;
									case 14:
										printf("enter the elent to find");
										scanf("%d",&item);
										search(start,item);
										break;
										case 15:
											display(start);
											break;
											case 16:
												 count(start);
												 break;
												 case 17:
												 	exit(1);
												 default:printf("wrong choice");
		}
		
		
		
	}
	
}

struct node *createlist(struct node *start){
	int data,i,n;
	start=NULL;
	printf("enter no. of node");
	scanf("%d",&n);
	if(n == 0)
	{
		return start;
	}
		printf("enter data");
	scanf("%d",&data);
	start=addatbeg(start,data);
	for(i=2;i<=n;i++){
		printf("enter data");
		scanf("%d",&data);
		start=addatend(start,data);
	}
	return start;

	
}
struct node *addatbeg(struct node *start,int data){
	struct node *tmp;
	tmp=(struct node *)malloc(sizeof(struct node));
	tmp->info=data;
	tmp->link=start;
	start=tmp;
	return start;

}
struct node *addatend(struct node *start,int data){
	struct node *tmp,*p;
	if(start == NULL){
		printf("empty list");
		return start;
	}
	tmp=(struct node *)malloc(sizeof(struct node));
	tmp->info=data;
	p=start;
	while(p->link != NULL){
		
		p=p->link;
	}
	
	p->link=tmp;
tmp->link=NULL;
return start;

}

struct node *addatpos(struct node *start,int data,int pos){
	int i;
	struct node *tmp,*p;
	tmp=(struct node*)malloc(sizeof(struct node));
		tmp->info=data;
	if(pos == 1){
		
		tmp->link=start;
		start=tmp;
		return start;
		
	}
	p=start;
	for(i=0;i<pos-1 && p!= NULL;i++){
		p=p->link;
	}
	if(p == NULL){
		printf("less no. of node");
		
	}
	else{
		tmp->link=p->link;
		p->link=tmp;
	}
	return start;
}
struct node *addafter(struct node *start,int data,int item){
	struct node*p,*tmp;
	p=start;
	while(p!=NULL)
	{
		if(p->info == item){
			tmp=(struct node *)malloc(sizeof(struct node));
			tmp->info=data;
	tmp->link=p->link;
	p->link=tmp;
	return start;
		}
		p=p->link;
	}
	printf("%d not found",item);
	return start;
	
}
struct node *addbefore(struct node *start,int data,int item){
	struct node *tmp,*p;
	if(start == NULL){
		printf("empty list");
		return start;
	}
	if(start->info == item){
		tmp=(struct node *)malloc(sizeof(struct node));
			tmp->info=data;
			tmp->link=start;
			start=tmp;
			return start;
	}
	while(p->link!=NULL){
		if(p->link->info == item){
			tmp=(struct node *)malloc(sizeof(struct node));
			tmp->info=data;
			tmp->link=p->link;
			p->link=tmp;
			return start;
			
		}
		p=p->link;
		
		
	}
	printf("%d not found",item);
	return start;
	
}
struct node *reverse(struct node *start){
	struct node *ptr,*prev,*next;
	ptr=start;
	prev=NULL;
	while(ptr != NULL){
		next=ptr->link;
		ptr->link=prev;
		prev=ptr;
		ptr=next;
		
	}
	start=prev;
	return start;
}
struct node *deleteduplicates(struct node *start){
	struct node *p,*q,*tmp;

for(p=start;p != NULL;p=p->link){
	for(q=start;q->link != NULL; q=q->link){
		if(p->info == q->link->info){
			tmp=q->link;
			q->link=tmp->link;
			free(tmp);
		}
	}
}
return start;
}
struct node *deletefirstnode(struct node *start){
	struct node *tmp;
	tmp=start;
	start=tmp->link;
	free(tmp);
	return start;
	
}

struct node *deletelastnode(struct node *start){
	struct node *p,*tmp;
	p=start;
	while(p->link != NULL){
		p=p->link;
	}

p->link = p->link->link;
	return start;
	
}
struct node *deleteitem(struct node *start,int data){
	struct node *p,*tmp;
	p=start;
	while(p->link != NULL){
		if(p->link->info == data){
			tmp=p->link;
			p->link=tmp->link;
			free(tmp);
			return start;
			
		}
	p=p->link;	
	}
	printf("not fornd");
}
struct node *deletepos(struct node *start,int pos){
	struct node *p,*tmp;
	int i;
	p=start;
	if(pos == 1){
		tmp=start;
		start=start->link;
		free(tmp);
		return start;
		
	}
	for(i=0;i<pos-1 && p!=NULL;i++){
		p=p->link;
	}
	if(p==NULL){
		printf("less no. of digits");
		return start;
	}
	else{
		tmp=p->link;
		p->link=tmp->link;
		free(tmp);
		return start;
	}
}
struct node *deletefull(struct node *start){
	struct node *tmp;
	tmp=start;
	start=tmp->link;
	free(tmp);
	return start;
}
void search(struct node *start,int item){
	int i=1;
	struct node *p;
	p=start;
	while(p!=NULL){
		if(p->info == item){
			printf("item found at %d",i);
			
		}
		p=p->link;
		i++;
	}
	printf("not found");
}
void display(struct node *start){
	struct node *p;
	p=start;
	while(p != NULL){
	printf("data: %d \n",p->info);
	p=p->link;
	}
}
void count(struct node *start){
	int cnt=0;
	struct node *p;
	while(p!=0){
		p=p->link;
		cnt++;
	}
	printf("count is : %d",cnt);
	
}
