## 题目描述
给定一个非负整数 numRows，生成杨辉三角的前 numRows 行。
思路分析

1.杨辉三角的性质

<img width="357" height="283" alt="image" src="https://github.com/user-attachments/assets/d903fb2c-6a6a-4b6c-9e58-e6f35da3d99c" />

性质：
- 每一行的第一个数字和最后一个数字是1
- 中间的数字 = 上一行的左上方数字 + 上一行的右上方数字
- triangle[i][j] = triangle[i-1][j-1] + triangle[i-1][j]

思路：
- 第一行一开始就初始化：[1]
- 对于第i行
  - 第一个元素是1
  - 中间元素：按上面的等式计算
  - 最后一个元素是1

数据结构
  - 二维数组存储结果
  - vector<vector<int>> triangle;

代码认识

**vector<int> row(i+1,1)**

- 初始大小为 i + 1 个元素

  - 每个元素的值都初始化为 1
  - 第0行是1个元素

**triangle.push_back(row);**

- 这行代码的意思是：将 row 这个向量添加到 triangle 的末尾
 
- 想象 triangle 是一个书架（书架可以放很多本书）
  - （triangle）：整个书架
  - row：一本书
  - push_back(row)：把这本书放到书架的最后一层

**i，j**

- i：表示第 i 行（从 0 开始计数）

- j：表示这一行的第 j 个元素（从 0 开始计数）



  代码

  ```
  vector <vector<int>> triangle;
  for(int i = 0;i<numRows;i++)------->我在这里的体会是，要一层一层计算到这一层，所以必须要从上面0开始算
  {
    vector<int> row(i+1,1);---------->这一步先把三角形搭起来，里面具体有什么下一步计算
    for(int j =1 ; j<i;j++)----->i行一共有i+1个元素，但是最后一个为1，也就是说j只能到i-1；不算第0个，默认是1
    {
      row[j] = triangle[i-1][j-1] + triangle[i-1][j];
      //这里第i行，所以要通过第i-1行来计算，
    }
  triangle.push_back(row);
  }
  return triangle;

  ```
