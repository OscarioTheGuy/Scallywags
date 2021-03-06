﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScallyWags
{
    public class Destructable : MonoBehaviour, IDamageable
    {
        [SerializeField] private GameObject _particleSystem;
        public void TakeDamage()
        {
            var ship = GetComponentInParent<ShipCondition>();
            ship?.TakeDamage();
            
            Instantiate(_particleSystem, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }
}