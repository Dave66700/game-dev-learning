# 160.相交链表

给你两个单链表的头节点 headA 和 headB ，请你找出并返回两个单链表相交的起始节点。

如果两个链表不存在相交节点，返回 null 。

<img width="468" height="601" alt="image" src="https://github.com/user-attachments/assets/7d00d93c-19bf-48b7-8fd1-9e08674b0596" />


核心：通过让两个指针遍历“对方的链表”，来消除这种长度差，从而让它们能在相交点相遇。

## 步骤

- 初始化指针
- 同步遍历
- 相遇得到答案
  - 相交则会在交点处相遇
  - 否则同时走到末尾，不相遇
 
## 代码

先要判断是不是空指针：

if(headA == nullptr || headB == nullptr) return nullptr;

两个指针不相等，则继续循环，相等就退出循环，此时返回相等的指针就ok

也就是返回aptr

判断之后，我要看aptr是不是空，如果是空则说明遍历完了，从而要让aptr指向headB

从而消除数量差

但是没空之前要执行aptr->next

```
ListNode *getIntersectionNode(ListNode *headA, ListNode *headB) {
    if (headA == nullptr || headB == nullptr) return nullptr;
    
    ListNode* aptr = headA;
    ListNode* bptr = headB;
    
    // 当两个指针不相等时继续循环
    while (aptr != bptr) {
        // aptr移动
        if (aptr == nullptr) {
            aptr = headB;  // 走到A末尾，转到B开头
        } else {
            aptr = aptr->next;
        }
        
        // bptr移动
        if (bptr == nullptr) {
            bptr = headA;  // 走到B末尾，转到A开头
        } else {
            bptr = bptr->next;
        }
    }
    
    // 返回相遇的节点（可能是交点，也可能是nullptr）
    return aptr;
}
```

```
/**
 * Definition for singly-linked list.
 * struct ListNode {
 *     int val;
 *     ListNode *next;
 *     ListNode(int x) : val(x), next(NULL) {}
 * };
 */
class Solution {
public:
    ListNode *getIntersectionNode(ListNode *headA, ListNode *headB)
    {
        if(headA == nullptr || headB == nullptr)
        return nullptr;

        ListNode* aptr= headA;
        ListNode* bptr = headB;

        while(aptr!=bptr)
        {
            if(aptr!=nullptr)
            {
                aptr=aptr->next;
            }
            else
            {
                aptr=headB;
            }

            if(bptr!=nullptr)
            {
                bptr = bptr->next;
            }
            else
            {
                bptr = headA;
            }
        }
        return aptr;
        
    }
};
```
