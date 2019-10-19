﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 持续型技能抽象类，所有具体持续型技能的父类。
/// 持续型技能在玩家按下时开始释放，到玩家松开按键时停止。
/// 该类的子类只需要实现LoadData、Start和End三个抽象方法即可，不需要做额外更改。
/// </summary>
public abstract class AbstractContinuousSkill : ISkill
{
    public SkillData Data { get; protected set; }
    // 施法者
    protected Unit Caster { get; private set; }
    // 技能特效产生位置
    protected Transform SpawnTransform { get; private set; }


    /// <summary>
    /// 根据具体技能，读取对应的技能数据。
    /// </summary>
    protected abstract void LoadData();

    /// <summary>
    /// 开始释放持续型技能
    /// </summary>
    protected abstract void Start();

    /// <summary>
    /// 停止释放持续型技能
    /// </summary>
    protected abstract void Stop();

    public void Init(Unit caster)
    {
        this.Caster = caster;
        this.SpawnTransform = caster.SpawnTransform;
        LoadData();
    }

    private bool isStarted = false;
    public void Trigger()
    {
        Debug.Log(string.Format("ID {0} continuous skill start at {1}", Caster.attributes.ID, Gamef.SystemTimeInMillisecond));
        if (isStarted)
        {
            Stop();
        }
        else
        {
            Start();
        }
        isStarted = !isStarted;
    }

    public abstract void AccuracyCooldown(float dt);
}
