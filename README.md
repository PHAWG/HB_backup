# Hearthbuddy_backed

Hearthbuddy 插件项目，包含 DefaultBot、DefaultRoutine（Silverfish AI）和多个实用插件。

## 项目结构

```
Hearthbuddy_backed/
├── Bots/                    # 机器人实现
│   └── DefaultBot/          # 默认机器人（天梯/休闲模式）
│       ├── DefaultBot.cs          # 主逻辑
│       ├── DefaultBotSettings.cs  # 设置类
│       ├── DefaultBotViewModel.cs # MVVM ViewModel
│       └── SettingsGui.xaml       # 设置界面
├── Routines/                # 策略实现
│   └── DefaultRoutine/      # 默认策略（Silverfish AI）
│       ├── DefaultRoutine.cs          # 主入口
│       ├── DefaultRoutineSettings.cs  # 设置类
│       ├── DefaultRoutineViewModel.cs # MVVM ViewModel
│       ├── SettingsGui.xaml           # 设置界面
│       └── Silverfish/                # AI 引擎
│           ├── ActionNormalizer.cs    # 动作规范化
│           ├── CardDB.cs              # 卡牌数据库
│           ├── PenalityManager.cs     # 惩罚管理器
│           ├── Playfield.cs           # 场景模拟
│           └── silverfish_HB.cs       # 主入口
├── Plugins/                 # 插件实现
│   ├── AutoStop/            # 自动停止插件
│   │   ├── AutoStop.cs
│   │   ├── AutoStopSettings.cs
│   │   ├── AutoStopViewModel.cs
│   │   └── SettingsGui.xaml
│   ├── Monitor/             # 监控插件
│   │   ├── Monitor.cs
│   │   ├── MonitorSettings.cs
│   │   ├── MonitorViewModel.cs
│   │   └── SettingsGui.xaml
│   ├── Quest/               # 任务插件
│   │   ├── Quest.cs
│   │   ├── QuestSettings.cs
│   │   ├── QuestViewModel.cs
│   │   └── SettingsGui.xaml
│   └── Stats/               # 统计插件
│       ├── Stats.cs
│       ├── StatsSettings.cs
│       ├── StatsViewModel.cs
│       └── SettingsGui.xaml
├── lib/                     # 第三方依赖库
├── CompilingDLLs/           # 编译输出目录
└── CompilingDLLs.sln        # 解决方案文件
```

## 功能特性

### DefaultBot（默认机器人）

- **多模式支持**: 天梯排名、休闲模式、经典模式、幻变模式
- **卡组管理**: 支持自定义卡组导入和缓存
- **自动打招呼**: 游戏开始时自动发送问候语
- **窗口管理**: 自动调整炉石窗口大小
- **自动投降**: 支持保持排名、互投、急速投降等模式

### DefaultRoutine（Silverfish AI）

- **智能出牌**: 基于场景模拟的最优决策
- **多种行为模式**: 控制、节奏、打脸等 AI 风格
- **防奥秘**: 自动规避对手奥秘
- **斩杀计算**: 精确计算斩杀线
- **表情系统**: 自动发送游戏表情

### 插件

| 插件 | 功能 |
|------|------|
| **AutoStop** | 达到指定场数/胜场/败场后自动停止，支持超时投降和动态打脸惩罚 |
| **Monitor** | 显示战令等级、经验值、运行时间、天梯排名等信息 |
| **Quest** | 显示每日/每周任务进度，支持刷新任务 |
| **Stats** | 统计各职业胜率和环境分布 |

## MVVM 架构重构 ✅ 已完成 (2026-05-04)

所有插件已完成 MVVM 架构重构：

### ViewModel 实现

| 插件 | ViewModel | 说明 |
|------|-----------|------|
| DefaultBot | DefaultBotViewModel | 管理对战模式、卡组、投降设置 |
| DefaultRoutine | DefaultRoutineViewModel | 管理 AI 行为、搜索参数、打脸奖励 |
| AutoStop | AutoStopViewModel | 管理停止条件、超时投降、动态惩罚 |
| Monitor | MonitorViewModel | 显示战令、排名、收藏品信息 |
| Quest | QuestViewModel | 管理任务进度和刷新 |
| Stats | StatsViewModel | 显示胜率和环境统计 |

### Settings 类详细注释

| 文件 | 注释内容 |
|------|----------|
| `DefaultBotSettings.cs` | 对战模式、卡组、投降设置 |
| `DefaultRoutineSettings.cs` | AI 行为、搜索参数、竞技场职业 |
| `AutoStopSettings.cs` | 停止条件、超时投降、动态打脸惩罚 |
| `MonitorSettings.cs` | 战令经验、运行统计、天梯排名 |
| `QuestSettings.cs` | 任务ID、进度、配额、经验、描述 |
| `StatsSettings.cs` | 各职业胜败场、胜率、环境分布 |

### 数据绑定修复

所有 ViewModel 已正确转发 Settings 的 `PropertyChanged` 事件，确保 UI 实时更新。

## 构建

```bash
dotnet restore CompilingDLLs.sln
dotnet build CompilingDLLs.sln
```

编译输出位于 `CompilingDLLs/` 目录。

## 使用方法

1. 编译项目生成 DLL 文件
2. 将 `CompilingDLLs/` 中的 DLL 复制到 Hearthbuddy 主程序的对应目录
3. 启动 Hearthbuddy，在设置中选择对应的 Bot/Routine/Plugin

## 依赖

- **Hearthbuddy 主程序**: 需要主程序提供 Triton 框架
- **log4net**: 日志记录
- **Newtonsoft.Json**: JSON 序列化

## 文档

- `.trae/specs/refactor-wpf-mvvm/CHANGELOG.md` — MVVM 重构详细说明文档

## 待办

- [ ] 完成连续操作，目前使用Queue储存待操作Action。应该判断下有操作是否合法，判断是否有嘲讽牌影响攻击。

### 已完成

- [x] 武器、英雄牌的默认使用，sim里只需要写战吼和亡语
- [x] 添加了"无法使用"的附魔效果和隐藏费用无法使用的卡牌判断
- [x] python读英雄皮肤的技能
- [x] 添加特殊随从类型，小鬼、树人、雏龙、小精灵
- [x] 添加callKidAndReturn方法，返回召唤的随从
- [x] 修复剪刀石头布和宝珠这类有特殊回合开始时的卡牌
- [x] 添加巨型召唤衍生物的方法
- [x] 奇利亚斯
- [x] 额外攻击次数
- [x] 通过tag查找原始卡牌，复制其sim

## 未完成

- [ ] 伊利斯
- [ ] 实现光环牌的效果
- [ ] 实现伤害来源，好判断吸血和剧毒

## 记录用

- 法力水晶上限和手牌上限
- MAXRESOURCES
- MAXHANDSIZE
- CORPSES 尸体数
- NUM_CARDS_PLAYED_THIS_TURN 这回合使用的卡牌数
- NUM_CARDS_DRAWN_THIS_TURN 这回合抽牌数
- MODULAR_ENTITY_PART_1
- MODULAR_ENTITY_PART_2
- 使用这两个tag记录自定义卡牌的模块
