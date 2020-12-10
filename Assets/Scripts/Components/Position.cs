using System;
using System.Collections.Generic;
using EpPathFinding.cs;
using Interfaces;
using UnityEngine;

namespace Components
{
    public class Position : IPosition
    {
    
        public event Action<GridPos> OnStepped;
        public event Action OnNearTarget;
        public event Action OnFarAwayTarget;

        private const float Offset = .2f; // максимальное отклонение при проверке на достижимость позиции
    
        private List<GridPos> _gridPoses;
        private int _currentPositionCount;
        private int _gridPosesCount;
        private GridPos _currentPosition;
        private float _maxDistance;

    
        //<summary>
        // Получение направляющего вектора
        //</summary>
        Vector3 IPosition.GetPosition()
        {
            if(_gridPosesCount == default || _gridPoses == null || _currentPositionCount > _gridPosesCount) return Vector2.zero;
            if(CheckPosition(new Vector3(_currentPosition.x, 0f, _currentPosition.y))) return Vector2.zero;
            var result = new Vector2(_gridPoses[_currentPositionCount].x, _gridPoses[_currentPositionCount].y);
            var prevGrid = _gridPoses[_currentPositionCount - 1];
            result = new Vector2(result.x - prevGrid.x, result.y - prevGrid.y);
            OnStepped?.Invoke(_currentPosition);
            return GetUnitDirectionVector(result);
        }

        void IPosition.SetStopDistance(float distance) => _maxDistance = distance;

        void IPosition.SetCurrentPosition(GridPos position) => _currentPosition = position;

        //<summary>
        // проверка на возможность пойти дальше
        //</summary>
        void IPosition.CheckAbleFurther(Vector3 currentPosition)
        {
            if(_gridPoses == null) return;
            if (_currentPositionCount < _gridPosesCount &&
                CheckPosition(currentPosition))
                _currentPositionCount++;
        }

        //<summary>
        // получение нового пути и обновление полей
        //</summary>
        void IPosition.SetTargetPosition(List<GridPos> gridPoses)
        {
            _gridPoses = gridPoses;
            _gridPosesCount = gridPoses.Count - 1;
            _currentPositionCount = 1;
        }
    
        //<summary>
        // Вычисление единичного вектора направления движения
        //</summary>
        private Vector3 GetUnitDirectionVector(Vector2 posesDifference)
        {
            var addVector = Vector3.zero;
            if (posesDifference.x > 0)
                addVector += new Vector3(1, 0);
            else if (posesDifference.x < 0)
                addVector -= new Vector3(1, 0);
            if (posesDifference.y > 0)
                addVector += new Vector3(0, 0, 1);
            else if(posesDifference.y < 0)
                addVector -= new Vector3(0, 0, 1);
            return addVector;
        }
    
        //<summary>
        // Проверка на достижение позиции
        //</summary>
        private bool CheckPosition(Vector3 position)
        {
            var targetPos = _gridPoses[_currentPositionCount];
            var targetPosition = new Vector3(targetPos.x, 0f, targetPos.y);
            targetPosition = CorrectionPath(position, targetPosition);
            return Mathf.Abs(position.x - targetPosition.x) < Offset &&
                   Mathf.Abs(position.z - targetPosition.z) < Offset;
        }

        //<summary>
        // Корректировка пути, если объект подошел на нужное расстаяние
        //</summary>
        private Vector3 CorrectionPath(Vector3 currentPosition, Vector3 targetPosition)
        {
            var targetGridPos = _gridPoses[_gridPosesCount];
            var target = new Vector3(targetGridPos.x, 0f, targetGridPos.y);
            if((target - currentPosition).magnitude <= _maxDistance)
            {
                targetPosition = currentPosition;
                OnNearTarget?.Invoke();
            } else
                OnFarAwayTarget?.Invoke();
            return targetPosition;
        }
    }
}