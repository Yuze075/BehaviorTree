# BehaviorTree
 
# 一、简介
  - 这个一个由个人基于Unity开发的行为树插件
  - 如果使用过程中，出现任何bug可以反馈给我QQ：925581968
# 二、安装
  - 将行为树的package导入unity即可
  - 注意：不要删除package的任何内容，这会导致插件无法运行
  - 注意：使用的unity版本必须是2021.3 or later
# 三、BehaviorTree组件
## 1. 添加
  - 在AddComponent中添加BehaviorTree脚本即可（会自动添加依赖脚本BlackBoard）

  ![AddComponent](/mdAssets/AddComponent.png)
## 2. 组成
  - 行为树组件包含两部分：
  - BehaviorTree脚本（行为树本体）
    - 用于处理行为树的逻辑
  - BlackBoard（共享黑板）
    - 用于行为树节点直接进行变量共享
  
  ![BehaviorTree](/mdAssets/BehaviorTree.PNG)
## 3. 使用
  - 在正常情况下，你只需要给予BehaviorTree脚本一个BehaviorTreeSo资产即可
  - 不需要对其他内容做任何配置（包括BlackBoard）
  - 注意如果未给予BehaviorTree脚本一个BehaviorTreeSo资产，则行为树将无法运行
## 4. 编辑
  - 在非运行模式下，点击“OpenEditorOnPlayMode”将没有任何效果
  - 在运行模式下，点击“OpenEditorOnPlayMode”将打开这个脚本对应的编辑器
    - 在编辑器中可以查看节点的状态，调整节点，进行Reset或者Pause
# 四、BehaviorTreeSo资产
- BehaviorTreeSo是用于存储对应行为树数据的一个资产
- 你应该给不同的行为树创建不同的BehaviorTreeSo资产
## 1. 创建
  - 在unity导航栏创建，点击BehaviorTree>CreateBehaviorTreeSo
  - 将在默认位置创建一个BehaviorTreeSo资产

  ![CreateBehaviorTreeSo](/mdAssets/CreateBehaviorTreeSo.png)
  - 在Create菜单创建，右键>Create>YuzeTool>BehaviorTreeSo
  - 在选中位置进行创建
  
  ![BehaviorTreeSo](/mdAssets/BehaviorTreeSo.png)
## 2. 编辑
  - 点击“OpenBehaviorTreeEditor”将打开编辑器，可以对so中的数据进行编辑
  
  ![BehaviorTreeSo](/mdAssets/BehaviorTreeSo.png)
# 五、编辑器
- 编辑器的界面一共分为三大部分
    
  ![Editor](/mdAssets/Editor.png)
## 1. Inspector
  - 当点击节点时，显示节点对应信息
## 2. TreeView
  - 显示行为树的区域，可以用于编辑行为树的结构和组成
  - 右键可以新建节点
    
  ![TreeView](/mdAssets/TreeView.png)
## 3. BlackBoard
  - 共享变量显示的窗口，可以用于删增变量
## 4. Toolbar
  - 在运行时，打开BehaviorTree脚本的编辑窗口额外显示的一个工具栏
  - 有两个按钮
    - Reset：在运行中重设整个行为树，重新开始
    - Pause：在运行中暂停（或者继续）整个行为树
    
  ![Toolbar](/mdAssets/Toolbar.png)
# 六、参考
  - https://github.com/mackysoft/Unity-SerializeReferenceExtensions
  - https://thekiwicoder.com/behaviour-tree-editor/