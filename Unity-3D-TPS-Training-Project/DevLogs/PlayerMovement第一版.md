# 代码部分

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    // 移动速度:public = 可以在 Inspector 里改
    public float gravity = -9.8f;
    //地球重力
    private CharacterController controller; 
    // 我有一个变量叫做controller 他能存储player的CharacterController
    private Vector3 velocity;


    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        //别忘了每帧执行
        Move();
        ApplyGravity();
    }
    void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        
        Vector3 move = transform.right * h + transform.forward * v;
        //transform.right--->角色的右方向
        //根据输入，算出移动方向的向量
        controller.Move(move*moveSpeed*Time.deltaTime);
        //move是方向、movespeed是速度

//补充一下Move相关
/*controller.Move 之所以能用，
 * 是因为 controller 是 CharacterController 类型，
 * 而 Move() 是 CharacterController 自带的方法
 * 我在前面写道：private CharacterController controller;
 * controller 变量只能存 CharacterController 类型的东西
 * 所以 controller 就“拥有 CharacterController 的全部方法和属性
 * 
 * 
 * 和以前不同的是：为什么不是直接用 transform.position
 * 玩家会穿过墙、穿过地面+不会自动检测斜坡+跳跃/重力处理麻烦
 * CharacterController.Move() 帮你处理碰撞和物理交互，只要你提供方向和速度。*/
}
    void ApplyGravity()
    {
        if (controller.isGrounded && velocity.y < 0)
            //角色在地面上吗？垂直速度是不是为负值？防止正在上升的时候重置速度
        {
            velocity.y = -2f;
            //-2f 是一个小的向下力，确保角色牢牢贴在地面上
        }

        velocity.y += gravity * Time.deltaTime;
        //但从式子理解：每时每刻（Time.deltaTime）竖直方向的速度都在叠加；
        //实际上就是V = Vo + g×Δt

        controller.Move(velocity * Time.deltaTime);

        //把“向下掉”的移动也交给角色
        // 这里面的velocity 通常包含：
        // velocity.x = 0 (通常水平速度由move控制)
        // velocity.y = 受重力影响的垂直速度（-值下落，+值上升）
        // velocity.z = 0 (通常水平速度由move控制)    
    }
}
```


## 关于代码的说明

 **关于CharacterController**
 
- 是什么？是一个胶囊体碰撞体。
     
- 带有 简单移动方法
     
- 内置 碰撞检测（角色不会穿墙或穿地面）和地面检测（是否在地面上）
     
- 不依赖 Rigidbody 物理系统
     
- Move（）传入向量就能移动并处理碰撞
     
**为什么不用Rigidbody？**

- Rigidbody，完全看物理引擎完全控制物理逼真，移动难以精准控制，射击体验不好。
    
- 对于CharacterController。程序控制移动，由玩家控制方向和速度。引擎控制碰撞。
     
     
 **CharacterController的使用**
 
- 给角色挂载：CharacterController组件
     
- controller = GetComponent<CharacterController>();
     
- 注意在start里面
  - 把 Player 身上的 CharacterController 存进 controller 变量里备用
 
**update函数**
 Move();
 
ApplyGravity();

