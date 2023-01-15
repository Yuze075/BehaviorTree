# BehaviorTree
 
# 一、简介
  - 这个一个由个人基于Unity开发的行为树插件
  - 如果使用过程中，出现任何bug可以反馈给我QQ：925581968
  - 详细使用文档可以看这个：https://blueregion.feishu.cn/wiki/wikcnQQhtMcychf3MgfwEyCn04D
# 二、快速开始
## 1. 安装
  - 将行为树的package导入unity即可
  - 注意：不要删除package的任何内容，这会导致插件无法运行
  - 注意：使用的unity版本必须是2021.3 or later
## 2. BehaviorTree组件
### 2.1. 添加
  - 在AddComponent中添加BehaviorTree脚本即可（会自动添加依赖脚本BlackBoard）

  ![AddComponent](/mdAssets/AddComponent.png)
### 2.2. 组成
  - 行为树组件包含两部分：
  - BehaviorTree脚本（行为树本体）
    - 用于处理行为树的逻辑
  - BlackBoard（共享黑板）
    - 用于行为树节点直接进行变量共享
  
  ![BehaviorTree](/mdAssets/BehaviorTree.PNG)
### 2.3. 使用
  - 在正常情况下，你只需要给予BehaviorTree脚本一个BehaviorTreeSo资产即可
  - 不需要对其他内容做任何配置（包括BlackBoard）
  - 注意如果未给予BehaviorTree脚本一个BehaviorTreeSo资产，则行为树将无法运行
### 2.4. 编辑
  - 在非运行模式下，点击“OpenEditorOnPlayMode”将没有任何效果
  - 在运行模式下，点击“OpenEditorOnPlayMode”将打开这个脚本对应的编辑器
    - 在编辑器中可以查看节点的状态，调整节点，进行Reset或者Pause
## 3. BehaviorTreeSo资产
- BehaviorTreeSo是用于存储对应行为树数据的一个资产
- 你应该给不同的行为树创建不同的BehaviorTreeSo资产
### 3.1. 创建
  - 在unity导航栏创建，点击BehaviorTree>CreateBehaviorTreeSo
  - 将在默认位置创建一个BehaviorTreeSo资产

  ![CreateBehaviorTreeSo](/mdAssets/CreateBehaviorTreeSo.png)
  - 在Create菜单创建，右键>Create>YuzeTool>BehaviorTreeSo
  - 在选中位置进行创建
  
  ![BehaviorTreeSo](/mdAssets/BehaviorTreeSo.png)
### 3.2. 编辑
  - 点击“OpenBehaviorTreeEditor”将打开编辑器，可以对so中的数据进行编辑
  
  ![BehaviorTreeSo](/mdAssets/BehaviorTreeSo.png)
## 4. 编辑器
- 编辑器的界面一共分为三大部分
    
  ![Editor](/mdAssets/Editor.png)
### 4.1. Inspector
  - 当点击节点时，显示节点对应信息
### 4.2. TreeView
  - 显示行为树的区域，可以用于编辑行为树的结构和组成
  - 右键可以新建节点
    
  ![TreeView](/mdAssets/TreeView.png)
### 4.3. BlackBoard
  - 共享变量显示的窗口，可以用于删增变量
### 4.4. Toolbar
  - 在运行时，打开BehaviorTree脚本的编辑窗口额外显示的一个工具栏
  - 有两个按钮
    - Reset：在运行中重设整个行为树，重新开始
    - Pause：在运行中暂停（或者继续）整个行为树
    
  ![Toolbar](/mdAssets/Toolbar.png)
# 三、Node节点
## 1. 接口和基类
### 1.1 INode

  ![Node](/mdAssets/Node.jpg)
  - 是所有Node都需要实现的接口
  - 里面包含了Node可以调用的所有公开函数
### 1.2 Node
  - 所有Node的抽象基类
  - 里面包含了Node可以调用的所有公开函数
  - 也包含了一些可以继承重新的函数
## 2. 函数
### 2.1 `Awake/OnEnable/Start/OnDiseable/OnDestory`
  - 和MonoBehaviour对应函数相同
  - 在对应时刻调用
### 2.2 `Update`
  - 行为树的逻辑更新函数，默认在MonoBehaviour.Update调用
  - 需要返回一个节点状态
    - BtState.Success：代表节点运行成功
    - BtState.Failure：代表节点运行失败
    - BtState.Running：代表节点正在运行
  - OnStartUpdate和OnEndUpdate在这次行为结束之前只执行一次（即返回Success或者Failure
### 2.3 `FixedUpdate&OnXXXStay`
  - 这两种函数只会去调用返回状态为BtState.Running的节点
  - 作用和和MonoBehaviour对应函数相同
  - 在对应时刻调用
### 2.4 `LateUpdate&OnXXXEnter&OnXXXExit`
  - 这两种函数只会去调用运行了的节点
  - 作用和和MonoBehaviour对应函数相同
  - 在对应时刻调用
### 2.5 `Reset`
  - 重设节点，让节点回到初始状态
### 2.6 `Pause(bool)`
  - 暂停或者解除暂停节点
  - 传入ture：暂停节点
  - 传入false：解除暂停
### 2.7 `Abort`
  - 打断节点运行，和Reset类似可以重置节点状态
  - 但是不强制，按照节点类型处理
## 3. 变量
| 类型 | 名称 | 说明 |
|------|-----|------|
|GameObject|gameObject|游戏对象主体|
|IBehaviorTree|behaviorTree|行为树主体|
|IBlackBoard|blackBoard|共享黑板主体（可以用于绑定动态创建的变量|
|int|NodeId|在这个行为树中每个节点的唯一标识，和Nodes中的顺序对应（可以通过behaviorTree.Nodes[NodeId]调用到这个节点|
|BtState|State|节点的状态，可以在OnUpdate中暂存一个状态|
## 4. 节点
### 4.1. Action
  - 执行具体行为的节点，没有子节点
  - 没有额外的函数
### 4.2. Composite
  - 组合节点，有多个子节点按照规则对子节点进行调用
### 4.3. Conditional
  - 判断节点，有一个判断的函数，返回一个bool值作为最后的状态
### 4.4. Decorator
  - 修饰节点，只有一个子节点，对子节点进行修饰
## 5.构建一个行为树

![start1](/mdAssets/start1.gif)

![start2](/mdAssets/start2.gif)
## 6.实现节点
- 插件内置了unity脚本模板，可以通过模板快速创建对应类型的节点

![Create](/mdAssets/Create.png)
### 6.1. Action
```C#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YuzeToolkit.BehaviorTree.Runtime;
[System.Serializable]
[AddTypeMenu("Action/NewAction")]
public class NewAction : Action
{
    protected override BtState OnUpdate()
    {
        // do something!!!

        return BtState.Success;
    }
}
```
### 6.2. Composite
|类型|名称|说明|
|---|----|----|
|List<INode>|Children|所有子节点的列表|
|int|Count|子节点的数量|
|int|SelectIndex|当前选择子节点的编号（调整这个可以调整选中的子节点|
|INode|SelectChild|当前选择的子节点|
```C#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YuzeToolkit.BehaviorTree.Runtime;
[System.Serializable]
[AddTypeMenu("Composite/NewComposite")]
public class NewComposite : Composite
{
    protected override BtState OnUpdate()
    {
        SelectIndex = 0;
        return SelectChild.Update();
    }
}
```
### 6.3. Conditional
```C#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YuzeToolkit.BehaviorTree.Runtime;
[System.Serializable]
[AddTypeMenu("Conditional/NewConditional")]
public class NewConditional : Conditional
{
    protected override bool IsConditional()
    {
        // 判断调整是否成立
    }
}
```
### 6.4. Decorator
|类型|名称|说明|
|---|----|----|
|INode|Child|子节点|
```C#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YuzeToolkit.BehaviorTree.Runtime;
[System.Serializable]
[AddTypeMenu("Decorator/NewDecorator")]
public class NewDecorator : Decorator
{
    protected override BtState OnUpdate()
    {
        // do something!!!

        return BtState.Success;
    }
}
```
# 四、BehaviorTree脚本
## 1. UpdateType
- 决定行为树的逻辑判断在哪个函数进行(即INode.Update)
### 1.1. Update
  - 逻辑在BehaviorTree.Update中进行更新
### 1.2. FixedUpdate
  - 逻辑在BehaviorTree.FixedUpdate中进行更新
  - 并且INode.FixedUpdate仍然在BehaviorTree.FixedUpdate中进行更新
### 1.3. LateUpdate
  - 逻辑在BehaviorTree.LateUpdate中进行更新
  - 并且INode.LateUpdate仍然在BehaviorTree.LateUpdate中进行更新
## 2. Pause&Reset
### 2.1. Pause
  - 设置BehaviorTree.PauseStatus可以设置暂停状态
  - 暂停或者解除暂停处于BtState.Running的节点
  - 传入一个bool值，表示现在是暂停还是解除暂停
  - 暂停状态下整个行为树将不再运行
### 2.2. Reset
  - 重设整个行为树，让其回到最开始的状态
## 3. FixedUpdate
- 在BehaviorTree.FixedUpdate只会更新返回BtState.Running的节点的INode.FixedUpdate函数
- 如果需要更新节点的INode.FixedUpdate函数，请使用下面这样的写法
- (这个判断条件同样适用于：OnTriggerStay/OnTriggerStay2D/OnCollisionStay/OnCollisionStay2D)
``` c#
namespace YuzeToolkit.BehaviorTree.Runtime
{
    [System.Serializable]
    [AddTypeMenu("Action/MyAction")]
    public class MyAction: Action
    {
        private bool _isFixedUpdate;
        
        public override void FixedUpdate()
        {
            // do something
            
            _isFixedUpdate = true;
        }
        
        protected override void OnStartUpdate()
        {
            _isFixedUpdate = false;
        }
        
        protected override BtStatus OnUpdate()
        {
            // do something
            return _isFixedUpdate ? MyStatus : BtStatus.Running;
        }
    }
}
```
# 五、BlackBoard黑板
## 1. 添加变量
  - 点击右下角的“+”就可以在BlackBoard中添加变量
  - 点击null的下拉条，就可以对这个变量的类型进行分配

  ![Add](/mdAssets/Add.png)

  ![SelectType](/mdAssets/SelectType.png)
## 2. 设置变量
  - name：变量的名称，用于共享变量时的绑定
  - variabel：变量的值，用于存储对应类型的数据

  ![ReName](/mdAssets/ReName.png)
## 3. 绑定变量
  - 在Node的Inspector面板中的变量会和BlackBoard中同类型的同名变量会绑定在一起
  - 绑定是通过反射自动完成的，无需手动操作
  - 如果变量没有名字则不会进行绑定
  - 绑定的值为BlackBoard的值，节点设置的值会被覆盖

  ![Bind](/mdAssets/Bind.png)
## 4. 绑定动态创建的变量
  - 虽然不推荐这么做，但是这里展示一下操作方法
``` C#
value.BindValueFromBlackboard(blackBoard);

blackBoard.BindValue(value);
```
# 六、参考
  - https://github.com/mackysoft/Unity-SerializeReferenceExtensions
  - https://thekiwicoder.com/behaviour-tree-editor/