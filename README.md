# HB\_backup

## 代办

- 完成连续操作，目前使用Queue储存待操作Action。应该判断下有操作是否合法，判断是否有嘲讽牌影响攻击。

### 已完成

- [x] 武器、英雄牌的默认使用，sim里只需要写战吼和亡语
- [x] 添加了“无法使用”的附魔效果和隐藏费用无法使用的卡牌判断
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

\##记录用

- 法力水晶上限和手牌上限
- MAXRESOURCES
- MAXHANDSIZE
- CORPSES 尸体数
- NUM\_CARDS\_PLAYED\_THIS\_TURN 这回合使用的卡牌数
- NUM\_CARDS\_DRAWN\_THIS\_TURN 这回合抽牌数
- MODULAR\_ENTITY\_PART\_1
- MODULAR\_ENTITY\_PART\_2
- 使用这两个tag记录自定义卡牌的模块

