using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AllMinions : MonoBehaviour
{
    public List<BaseCard> OneStarList = new List<BaseCard>();
    public List<BaseCard> TwoStarList = new List<BaseCard>();
    public List<BaseCard> ThreeStarList = new List<BaseCard>();
    public List<BaseCard> FourStarList = new List<BaseCard>();
    public List<BaseCard> FiveStarList = new List<BaseCard>();
    public List<BaseCard> SixStarList = new List<BaseCard>();
    public List<BaseCard> OneManaList = new List<BaseCard>();
    public List<BaseCard> TwoManaList = new List<BaseCard>();
    public List<BaseCard> FourManaList = new List<BaseCard>();
    public List<BaseCard> LengendList = new List<BaseCard>();
    public List<BaseCard> DeathrattleList = new List<BaseCard>();    

    public List<BaseCard> GoldOneStarList = new List<BaseCard>();
    public List<BaseCard> GoldTwoStarList = new List<BaseCard>();
    public List<BaseCard> GoldThreeStarList = new List<BaseCard>();
    public List<BaseCard> GoldFourStarList = new List<BaseCard>();
    public List<BaseCard> GoldFiveStarList = new List<BaseCard>();
    public List<BaseCard> GoldSixStarList = new List<BaseCard>();

    public List<BaseCard> TokenList = new List<BaseCard>();
    public List<BaseCard> GoldenTokenList = new List<BaseCard>();

    public BaseCard GetCardByInt(int ID)
    {        
        if (ID <= 7000)
        {
            int[] IDstr = new int[4];

            IDstr[0] = ID / 1000;
            int tmp = ID % 1000;
            IDstr[1] = tmp / 100;
            tmp = tmp % 100;
            IDstr[2] = tmp / 10;
            tmp = tmp % 10;
            IDstr[3] = tmp;

            Debug.Log(IDstr[0] + "" + IDstr[1] + "" + IDstr[2] + "" + IDstr[3]);
            return GetCardByID(IDstr);
        }
        else
        {
            return null;
        }
    }

    public BaseCard GetCardByID(int[] ID) //此处ID应为4位
    {
        if (ID.Length == 4)
        {
            int tmp = 10 * ID[1] + ID[2];
            if (ID[3] == 0)
            {
                if (ID[0] == 0)
                {
                    if (tmp >= 0 && tmp <= TokenList.ToArray().Length)
                    {
                        return TokenList[tmp];
                    }
                }
                else if (ID[0] == 1)
                {
                    if (tmp >= 0 && tmp <= OneStarList.ToArray().Length)
                    {
                        return OneStarList[tmp];
                    }
                }
                else if (ID[0] == 2)
                {
                    if (tmp >= 0 && tmp <= TwoStarList.ToArray().Length)
                    {
                        return TwoStarList[tmp];
                    }
                }
                else if (ID[0] == 3)
                {
                    if (tmp >= 0 && tmp <= ThreeStarList.ToArray().Length)
                    {
                        return ThreeStarList[tmp];
                    }
                }
                else if (ID[0] == 4)
                {
                    if (tmp >= 0 && tmp <= FourStarList.ToArray().Length)
                    {
                        return FourStarList[tmp];
                    }
                }
                else if (ID[0] == 5)
                {
                    if (tmp >= 0 && tmp <= FiveStarList.ToArray().Length)
                    {
                        return FiveStarList[tmp];
                    }
                }
                else if (ID[0] == 6)
                {
                    if (tmp >= 0 && tmp <= SixStarList.ToArray().Length)
                    {
                        return SixStarList[tmp];
                    }
                }
                else
                {
                    Debug.Log("Please enter the correct ID");
                    return null;
                }
            }
            else if (ID[3] == 1)
            {
                if (ID[0] == 0)
                {
                    if (tmp >= 0 && tmp <= GoldenTokenList.ToArray().Length)
                    {
                        return GoldenTokenList[tmp];
                    }
                }
                else if (ID[0] == 1)
                {
                    if (tmp >= 0 && tmp <= GoldOneStarList.ToArray().Length)
                    {
                        return GoldOneStarList[tmp];
                    }
                }
                else if (ID[0] == 2)
                {
                    if (tmp >= 0 && tmp <= GoldTwoStarList.ToArray().Length)
                    {
                        return GoldTwoStarList[tmp];
                    }
                }
                else if (ID[0] == 3)
                {
                    if (tmp >= 0 && tmp <= GoldThreeStarList.ToArray().Length)
                    {
                        return GoldThreeStarList[tmp];
                    }
                }
                else if (ID[0] == 4)
                {
                    if (tmp >= 0 && tmp <= GoldFourStarList.ToArray().Length)
                    {
                        return GoldFourStarList[tmp];
                    }
                }
                else if (ID[0] == 5)
                {
                    if (tmp >= 0 && tmp <= GoldFiveStarList.ToArray().Length)
                    {
                        return GoldFiveStarList[tmp];
                    }
                }
                else if (ID[0] == 6)
                {
                    if (tmp >= 0 && tmp <= GoldSixStarList.ToArray().Length)
                    {
                        return GoldSixStarList[tmp];
                    }
                }
                else
                {
                    Debug.Log("Please enter the correct ID");
                    return null;
                }
            }
            else
            {
                Debug.Log("Please enter the correct ID");
                return null;
            }
        }
        else
        {
            Debug.Log("Please enter the correct ID");
            return null;
        }
        Debug.Log("Please enter the correct ID");
        return null;
    }

    public BaseCard GetTokenByID()
    {
        return null;
    }
    
    /* 随从表 使用四位序列号表示，第一位表示星级，中间两位为序号，第四位表示随从是否为金色
     * 1000 雄斑虎           Alleycat                          0000
     * 1010 恐狼前锋         Dire Wolf Alpha
     * 1020 邪魔仆从         Fiendish Servant
     * 1030 机械袋鼠         Mecharoo                          0010
     * 1040 微型战斗机甲     Micro Machine
     * 1050 鱼人招潮者       Murloc Tidecaller
     * 1060 鱼人猎潮者       Murloc Tidehunter                 0020
     * 1070 正义保护者       Righteous Protector
     * 1080 石塘猎人         Rockpool Hunter
     * 1090 无私的英雄       Selfless Hero
     * 1100 粗俗的矮劣魔     Vulgar Homunculus
     * 1110 愤怒编织者       WrathWeaver
     *                       
     * 2000 吵吵机器人       Annoy-o-Tron
     * 2010 麦田傀儡         Harvest Golem                     0030
     * 2020 小鬼囚徒         Imprisoner                        0040
     * 2030 爆爆机器人       Kaboom Bot
     * 2040 慈祥的外婆       Kindly GrandMother                0050
     * 2050 金刚刃牙兽       Metaltooth Leaper                   
     * 2060 骑乘迅猛龙       Mounted Raptor
     * 2070 鱼人领军         Murloc Warleader
     * 2080 纳斯雷兹姆监工   Nathrezim Overseer
     * 2090 老瞎眼           Old Murk-Eye 
     * 2100 蹦蹦兔           Pogo-Hopper
     * 2110 瘟疫鼠群         Rat Pack                          0060
     * 2120 食腐土狼         Scavenging Hyena
     * 2130 护盾机器人       Shielded Minibot
     * 2140 恩佐斯的子嗣     Spawn of N'Zoth
     * 2150 机械动物管理员   Zoobot
     *                       
     * 3000 钴制卫士         Cobalt Guardian
     * 3010 寒光先知         Coldlight Seer
     * 3020 人气选手         Crowd Favorite
     * 3030 魔瘾结晶者       Crystalweaver
     * 3040 驯兽师           Houndmaster
     * 3050 小鬼首领         Imp Gang Boss
     * 3060 寄生恶狼         Infested Wolf                     0070
     * 3070 卡德加           Khagar
     * 3080 族群领袖         Pack Leader
     * 3090 方阵指挥官       Phalanx Commander
     * 3100 载人收割机       Piloted Shredder
     * 3110 闹闹机器人       Psych-o-Tron
     * 3120 量产型恐吓机     Replicating Menace                0080
     * 3130 废旧螺栓机甲     Screwjank Clunker
     * 3140 百变泽鲁斯       Shifter Zerus
     * 3150 灵魂杂耍者       Soul Juggler
     * 3160 比斯巨兽         The Beast                         0090
     * 3170 始祖龟执盾者     Tortollan Shellraiser
     *                       
     * 4000 吵吵模组         Annoy-o-Module
     * 4010 浴火者伯瓦尔     Bolvar,Fireblood
     * 4020 洞穴多头蛇       Cave Hydra
     * 4030 阿古斯防御者     Defender of Argus
     * 4040 腐树巨人         Festeroot Hulk
     * 4050 钢铁武道家       Iron Sensei
     * 4060 机械蛋           Mechano-Egg                       0100
     * 4070 展览馆法师       Menagerie Magician
     * 4080 载人飞天魔像     Piloted Sky Golem
     * 4090 安保巡游者       Security Rover                    0110
     * 4100 攻城恶魔         Siegebreaker
     * 4110 波戈蒙斯塔       The Boogeymonster
     * 4120 毒鳍鱼人         Toxfin
     * 4130 兔妖教头         Virmen Sensei
     * 4140 漂浮观察者       Floating Watcher
     *
     * 5000 安尼赫兰战场军官 Annihilan Battlemaster
     * 5010 瑞文戴尔男爵     Baron Rivendare
     * 5020 布莱恩·铜须     Brann Bronzebeard
     * 5030 巨狼戈德林       Goldrinn，the Great Wolf
     * 5040 铁皮恐角龙       Ironhide Direhorn                 0120
     * 5050 回收机器人       Junkbot
     * 5060 拜戈尔格国王     King Bagurgle
     * 5070 光牙执行者       Lightfang Enforcer
     * 5080 玛尔加尼斯       Mal'Ganis
     * 5090 蛮鱼斥候         Primalfin Lookout
     * 5100 臃肿的蛇颈龙     Sated Threshadon                  0130
     * 5110 长鬃草原狮       Savannah Highmane                 0140
     * 5120 硬壳清道夫       Intongshell Scavenger
     * 5130 虚空领主         VoidLord
     *
     * 6000 死神4000型       Foe Reaper 4000
     * 6010 温顺的巨壳龙     Gentle Megasaour                  0150
     * 6020 阴森巨蟒         Ghastcoiler
     * 6030 坎格尔的学徒     Kangor's Apprentice
     * 6040 迈克斯纳         Maexxna
     * 6050 熊妈妈           Mama Bear
     * 6060 斯尼德的伐木机   Sneed's Old Shredder
     * 6070 扎普 斯里维克    Zapp Slywick
     * 
     * 
     */

}
