using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.Events;
using System.Collections.Generic;

public class PickerWheel : MonoBehaviour 
{
    [Header("References :")]
    [SerializeField] private GameObject _linePrefab;
    [SerializeField] private Transform _linesParent;
    [SerializeField] private Transform _tranferPoint;

    [Space]
    [SerializeField] private Transform _pickerWheelTransform;
    [SerializeField] private Transform _wheelCircle;
    [SerializeField] private PieceView _pieceView;
    [SerializeField] private Transform _wheelPiecesParent;

    [Space]
    [Header("Sounds :")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _tickAudioClip;
    [SerializeField] [Range(0f, 1f)] private float _volume = 0.5f;
    [SerializeField] [Range(-3f, 3f)] private float _pitch = 1f;

    [Space]
    [Header("Picker wheel settings :")]
    [Range(1, 20)] public int _spinDuration = 8;
    [SerializeField] [Range(.2f, 2f)] private float _wheelSize = 1f;

    [Space]
    [Header("Picker wheel pieces :")]
    [SerializeField] private List<WheelPiece> _wheelPieces;

    private bool _isSpinning = false;
    private int _piecesMin = 2;
    private int _piecesMax = 12;
    private float _pieceAngle;
    private float _halfPieceAngle;
    private float _halfPieceAngleWithPaddings;
    private double _accumulatedWeight;
    private System.Random _rand = new System.Random();
    private List<int> _nonZeroChancesIndices = new List<int>();

    public event UnityAction<WheelPiece> SpinEnded;
    public event UnityAction<Vector3> PrizeReceived;

    public bool IsSpinning => _isSpinning;

    private void Start()
    {
        _pieceAngle = 360 / _wheelPieces.Count;
        _halfPieceAngle = _pieceAngle / 2f;
        _halfPieceAngleWithPaddings = _halfPieceAngle - (_halfPieceAngle / 4f);

        Generate();

        CalculateWeightsAndIndices();

        if (_nonZeroChancesIndices.Count == 0)
            throw new ArgumentOutOfRangeException();

        SetupAudio();
    }

    private void OnValidate()
    {
        if (_pickerWheelTransform != null)
            _pickerWheelTransform.localScale = new Vector3(_wheelSize, _wheelSize, 1f);

        if (_wheelPieces.Count > _piecesMax || _wheelPieces.Count < _piecesMin)
            throw new Exception("[ PickerWheelwheel ]  pieces length must be between " + _piecesMin + " and " + _piecesMax);
    }

    private void SetupAudio()
    {
        _audioSource.clip = _tickAudioClip;
        _audioSource.volume = _volume;
        _audioSource.pitch = _pitch;
    }

    private void Generate()
    {
        for (int i = 0; i < _wheelPieces.Count; i++)
            DrawPiece(i);         
    }

    private void DrawPiece(int index)
    {
        WheelPiece piece = _wheelPieces[index];
        Transform pieceTrns = InstantiatePiece(piece).transform.GetChild(0);
        
        Transform lineTrns = Instantiate(_linePrefab, _linesParent.position, Quaternion.identity, _linesParent).transform;
        
        lineTrns.RotateAround(_wheelPiecesParent.position, Vector3.back, (_pieceAngle * index) + _halfPieceAngle);
        pieceTrns.transform.RotateAround(_wheelPiecesParent.position, Vector3.back, _pieceAngle * index);
    }

    private GameObject InstantiatePiece(WheelPiece piece)
    {
        _pieceView.AddIcon(piece.Icon);
        _pieceView.AddText(piece.Amount);

        return Instantiate(_pieceView.gameObject, _wheelPiecesParent.transform);
    }

    public void Spin()
    {
        if (_isSpinning == false)
        {
            _isSpinning = true;

            int index = GetRandomPieceIndex();
            WheelPiece piece = _wheelPieces[index];

            if (piece.Chance == 0 && _nonZeroChancesIndices.Count != 0)
            {
                index = _nonZeroChancesIndices[UnityEngine.Random.Range(0, _nonZeroChancesIndices.Count)];
                piece = _wheelPieces[index];
            }

            float angle = -1 * (_pieceAngle * index);

            float rightOffset = (angle - _halfPieceAngleWithPaddings) % 360;
            float leftOffset = (angle + _halfPieceAngleWithPaddings) % 360;

            float randomAngle = UnityEngine.Random.Range(leftOffset, rightOffset);

            Vector3 targetRotation = Vector3.back * (randomAngle + 2 * 360 * _spinDuration);

            float prevAngle, currentAngle;
            prevAngle = currentAngle = _wheelCircle.eulerAngles.z;

            bool isIndicatorOnTheLine = false;

            _wheelCircle.DORotate(targetRotation, _spinDuration, RotateMode.Fast).SetEase(Ease.InOutQuart)
            .OnUpdate(() =>
            {
                float diff = Mathf.Abs(prevAngle - currentAngle);

                if (diff >= _halfPieceAngle)
                {
                    if (isIndicatorOnTheLine)
                        _audioSource.PlayOneShot(_audioSource.clip);

                    prevAngle = currentAngle;
                    isIndicatorOnTheLine = !isIndicatorOnTheLine;
                }

                currentAngle = _wheelCircle.eulerAngles.z;
            })
            .OnComplete(() =>
            {
                _isSpinning = false;

                PrizeReceived?.Invoke(_tranferPoint.position);
                SpinEnded?.Invoke(piece);
            });
        }
    }

    private int GetRandomPieceIndex() 
    {
         double r = _rand.NextDouble() * _accumulatedWeight;

        for (int i = 0; i < _wheelPieces.Count; i++)
            if (_wheelPieces[i].Weight >= r)
                return i;

        return 0;
    }

    private void CalculateWeightsAndIndices()
    {
        for (int i = 0; i < _wheelPieces.Count; i++)
        {
            WheelPiece piece = _wheelPieces[i];

            _accumulatedWeight += piece.Chance;
            piece.AddWeight(_accumulatedWeight);

            if (piece.Chance > 0)
                _nonZeroChancesIndices.Add(i);
        }
    }
}