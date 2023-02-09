using System;
using UnityEngine;

namespace Game
{
    public struct ShipData
    {
        public int id;
        public float base_dmg;
        public string name;
        public string kName;
        public int chr_level;
        public int locked;
        public float dmg;
        public float nextDmg;

        public ShipData(int id,float base_dmg,string name,string kName,int chr_level,int locked,float dmg = 1,float nextDmg = 1)
        {
            this.id = id;
            this.base_dmg = base_dmg;
            this.name = name;
            this.kName = kName;
            this.chr_level = chr_level;
            this.locked = locked;
            this.dmg = dmg;
            this.nextDmg = nextDmg;
        }

        public string GetImagName()
        {
            return $"Character/{id.ToString()}/0";
        }
        
        //chr_lever 추가시 이 함수를 꼭 실행
        public void SetDamage()
        {
            this.dmg = chr_level * base_dmg;
            this.nextDmg = (chr_level + 1) * base_dmg;
        }
        public void Show()
        {
            Debug.Log($"id : {id} base_dmg : {base_dmg} name : {name} kName : {kName}" +
                      $"chr_level : {chr_level} locked : {locked} dmg : {dmg}");
        }
    }
}
