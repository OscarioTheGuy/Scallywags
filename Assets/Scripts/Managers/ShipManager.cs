﻿using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace ScallyWags
{
    public class ShipManager : MonoBehaviour
    {
        private List<ShipCondition> ships = new List<ShipCondition>();
        private Transform _spawnPos;
        public void Init()
        {
            var ship = GameObject.FindObjectOfType<ShipCondition>();
            ship.Init(ShipType.Player, this, 10);
            ships.Add(ship);
            _spawnPos = GameObject.FindObjectOfType<EnemyShipSpawn>().gameObject.transform;
        }

        public void Tick()
        {
            foreach (var ship in ships)
            {
                ship.Tick();
            }
        }

        private void OnEnable()
        {
            EventManager.StartListening("EnemyShip", CreateShip);
        }
        
        private void OnDisable()
        {
            EventManager.StopListening("EnemyShip", CreateShip);
        }
        private void CreateShip(EventManager.EventMessage message)
        {
            CreateShipObject(message.HazardData.Prefab, ShipManager.ShipType.Enemy, 5);
        }

        private void CreateShipObject(GameObject prefab, ShipType shipType, int health)
        {
            foreach (var s in ships)
            {
                if (s.ShipType == shipType)
                {
                    return;
                }
            }
            
            var go = GameObject.Instantiate(prefab, _spawnPos.position, Quaternion.identity);
            var ship = go.GetComponent<ShipCondition>();
            ship.Init(shipType, this, health);
            ships.Add(ship);
        }

        public enum ShipType
        {
            Enemy,
            Player
        }

        public void RemoveShip(ShipCondition shipCondition)
        {
            if (ships.Contains(shipCondition))
            {
                ships.Remove(shipCondition);
            }
        }
    }
}
