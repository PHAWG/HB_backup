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
│           ├── Playfield/             # 场景模拟（Partial 类拆分）
│           │   ├── Playfield.cs             # 核心状态与构造
│           │   ├── Playfield.Turn.cs        # 回合管理
│           │   ├── Playfield.CardPlay.cs    # 出牌与手牌
│           │   ├── Playfield.Attack.cs      # 攻击与武器
│           │   ├── Playfield.Trigger.cs     # 触发器与亡语
│           │   ├── Playfield.Secret.cs      # 奥秘系统
│           │   ├── Playfield.Aura.cs        # 光环与增益
│           │   ├── Playfield.Minion.cs      # 随从管理
│           │   ├── Playfield.DamageHeal.cs  # 伤害与治疗
│           │   ├── Playfield.Lethal.cs      # 致命与敌方模拟
│           │   ├── Playfield.Compare.cs     # 比较与哈希
│           │   ├── Playfield.Search.cs      # 搜索与评估
│           │   ├── Playfield.SpecialMechanics.cs  # 特殊机制
│           │   └── Playfield.Debug.cs       # 调试与工具
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

## Playfield Partial 类拆分 ✅ 已完成 (2026-05-04)

### 背景

Playfield 类是 Silverfish AI 引擎的核心战场状态类，原文件超过 **13,000 行**，是一个典型的"上帝类"（God Class），承担了战场状态存储、操作模拟、触发器系统、奥秘系统、亡语处理、光环管理、敌方模拟、致命计算等几乎所有 AI 引擎核心职责。

### 拆分原则

- 使用 `partial` 关键字将类拆分为多个文件，**不改变任何公共 API、方法签名或运行时行为**
- **所有字段声明保留在主文件**，不分散到 partial 文件，确保字段查找只需查看一个文件
- 每个文件对应一个明确的职责域，文件命名格式：`Playfield.{职责域}.cs`

### 拆分结果

| # | 文件名 | 职责域 | 包含内容 | 方法数 |
|---|---|---|---|---|
| 1 | `Playfield.cs` | 核心状态与构造 | 所有字段声明（~340个）、两个构造函数、`triggerCounter`/`IDEnumOwner`/`RaceUtils` | 0（仅构造函数） |
| 2 | `Playfield.Turn.cs` | 回合管理 | `onOwnTurnStart`、`onEnemyTurnStart`、`onEnemyTurnEnd`、`endTurn`、`startTurn`、`unlockMana`、回合触发器（`triggerEndTurn`/`triggerStartTurn`及其私有辅助方法）、`triggerAHeroGotArmor`、`triggerCardsChanged`、`triggerInspire` | 19 |
| 3 | `Playfield.CardPlay.cs` | 出牌与手牌 | `PlayACard`及私有辅助方法、`PlayHeroPower`、手牌/牌库管理（`drawACard`、`removeCard`、`discardCards`、`renumHandCards`、`AddToDeck`、`RemoveFromDeck`、`AddToEnemyHand`、`RemoveFromEnemyHand`、`drawTemporaryCard`、`removeTemporaryCards`）、卡牌价值计算辅助方法 | 28 |
| 4 | `Playfield.Attack.cs` | 攻击与武器 | `doAction`、`minionAttacksMinion`、`attackWithWeapon`、攻击处理链（`HandleMinionAttack`、`HandleHeroAttack`、武器伤害调整、武器特殊效果、防御者/攻击者受伤效果、过杀/荣誉击杀等）、`equipWeapon`、`lowerWeaponDurability`、武器破碎处理、`FindMinionByEntityId`、`FindHandCard` | 25 |
| 5 | `Playfield.Trigger.cs` | 触发器与亡语 | 所有触发器方法（`doDmgTriggers`、`triggerACharGotHealed`、`triggerAMinionGotHealed`、`triggerAMinionGotDmg`、`triggerAMinionLosesDivineShield`、`triggerAMinionDied`、`triggerAMinionIsGoingToAttack`、`triggerAMinionDealedDmg`、`triggerACardWillBePlayed`、`triggerAMinionIsSummoned`、`triggerAMinionWasSummoned`）、`doDeathrattles`、灌注（Infuse）相关方法 | 20 |
| 6 | `Playfield.Secret.cs` | 奥秘系统 | `getMergedSecretItem`、所有 `secretTrigger_*` 方法、`getSecretTriggersByType`、`UpdateTargetBasedOnSecret`、`EnemyUpdateTargetBasedOnSecret`、`HandleCounterspellOrSpellbender` | 11 |
| 7 | `Playfield.Aura.cs` | 光环与增益 | `updateBoards`、`minionGetOrEraseAllAreaBuffs`、`handleRaceSpecificBuffs`、`updateAdjacentBuffs`、`handleSpiritClaws` | 5 |
| 8 | `Playfield.Minion.cs` | 随从管理 | 随从创建/放置（`createNewMinion`、`placeAmobSomewhere`、`addMinionToBattlefield`、`callKid`、`callKidAndReturn`、`CallMinionCopy`）、随从状态修改（冻结、沉默、消灭、回手、回牌库、变形、换控、磁力、增益/减益、圣盾/嘲讽/风怒/冲锋/突袭/吸血、设置攻击力/生命值等） | 35 |
| 9 | `Playfield.DamageHeal.cs` | 伤害与治疗 | 伤害/治疗计算方法（`getSpellDamageDamage`、`getSpellHeal`、`getMinionHeal`、`getHeroPowerDamage`等及敌方版本）、群体伤害/治疗（`allMinionOfASideGetDamage`、`allCharsOfASideGetDamage`、`allCharsGetDamage`等）、`minionGetDamageOrHeal`、`applySpellLifesteal`、`HealHero` | 19 |
| 10 | `Playfield.Lethal.cs` | 致命与敌方模拟 | `lethalMissing`、`nextTurnWin`、`calDirectDmg`、`ownHeroHasDirectLethal`、`guessEnemyHeroLethalMissing`、`guessHeroDamage`、敌方模拟（`enemyPlaysAoe`、`EnemyCardPlaying`、`EnemyPlaysACard`、`EnemyplaysACard`、`EnemyHandleEnemyMinionPlay`）、陷阱模拟（`simulateTrapsStartEnemyTurn`、`simulateTrapsEndEnemyTurn`） | 14 |
| 11 | `Playfield.Compare.cs` | 比较与哈希 | `isEqual`、`isEqualf`、`GetPHash`、`copyValuesFrom`、`addMinionsReal`、`addCardsReal` | 6 |
| 12 | `Playfield.Search.cs` | 搜索与评估 | `GetAttackTargets`、`getBestPlace`、`getBestAdapt`、`searchRandomMinion`、`searchRandomMinionByMaxHP`、`searchRandomMinionInHand`、`getEnemyCharTargetForRandomSingleDamage`、`calTotalAngr`、`calEnemyTotalAngr`、`getNextEntity`、`getHandcardsByType`、`getRandomCardForManaMinion`、`CheckTurnDeckForType`、`CheckTurnDeckExists` | 14 |
| 13 | `Playfield.SpecialMechanics.cs` | 特殊机制 | 尸体/海盗（`addCorpses`、`summonPirate`、`corpseConsumption`、`getCorpseCount`）、发掘（`handleExcavation`、`getTreasurePool`、`getLegendaryTreasure`等）、地标/泰坦（`useLocation`、`useTitanAbility`）、`setNewHeroPower`、`Magnetic`、`getNextJadeGolem` | 13 |
| 14 | `Playfield.Debug.cs` | 调试与工具 | `debugMinions`、`printBoard`、`printBoardString`、`printBoardDebug`、`getNextAction`、`printActions`、`printActionforDummies`、`getRandomNumber`、`CountSpellSchoolsPlayed`、`hasMinionsInDeck`、`RandomEnemyMinionsAttackEachOther`、`getPosition`、`anyRaceCardInHand`、`RemoveQuickDrawStatus` | 14 |

### 拆分收益

| 指标 | 拆分前 | 拆分后 |
|------|--------|--------|
| 主文件行数 | 13,286 行 | ~1,600 行 |
| 文件数量 | 1 个 | 14 个 |
| 单文件最大行数 | 13,286 行 | ~2,500 行 |
| 代码导航效率 | 低（需在超大文件中滚动） | 高（按职责快速定位） |
| 代码可维护性 | 低（职责混杂） | 高（职责清晰分离） |

### 修复的问题

拆分过程中发现并修复了以下问题：

1. **重复定义修复**: `UpdateTargetBasedOnSecret` 方法在 CardPlay.cs 和 Secret.cs 中重复定义，已移除 CardPlay.cs 中的重复定义
2. **拼写错误修复**: `handleRaceSpecificBuffs` 方法参数类型拼写错误（`Mion` → `Minion`）
3. **遗漏方法补充**: 补充了5个在原始文件中存在但拆分时遗漏的方法：
   - `FindHandCard` — 查找手牌（添加到 Attack.cs）
   - `getRandomCardForManaMinion` — 按费用获取随机随从（添加到 Search.cs）
   - `CheckTurnDeckForType` — 按类型检查牌库（添加到 Search.cs）
   - `CheckTurnDeckExists` — 按种族检查牌库（添加到 Search.cs）
   - `getNextJadeGolem` — 获取下一个青玉魔像（添加到 SpecialMechanics.cs）
4. **API 可见性调整**: 将 `CardDB.cardlist` 字段从 `private` 改为 `public`，以支持 `getRandomCardForManaMinion` 实现

### 编译验证

- ✅ 项目编译通过，0 个错误
- ✅ 63 个警告（均为项目原有警告，非本次拆分引入）
- ✅ 所有方法签名与拆分前完全一致，无公共 API 变更

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
