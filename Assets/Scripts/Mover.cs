using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private Transform AllPlacespoint;
    [SerializeField] private float _moveSpeed;

    private Transform[] arrayPlaces;
    private int _numberOfPlaceInArrayPlaces;

    private void Start()
    {
        CreateArray();
    }

    private void Update()
    {
        Move();
    }

    private void CreateArray()
    {
        arrayPlaces = new Transform[AllPlacespoint.childCount];

        for (int i = 0; i < AllPlacespoint.childCount; i++)
            arrayPlaces[i] = AllPlacespoint.GetChild(i).GetComponent<Transform>();
    }

    private void Move()
    {
        var pointByNumberInArray = arrayPlaces[_numberOfPlaceInArrayPlaces];
        transform.position = Vector3.MoveTowards(transform.position, pointByNumberInArray.position, _moveSpeed * Time.deltaTime);

        if (transform.position == pointByNumberInArray.position) NextPlaceTakerLogic();
    }

    private Vector3 NextPlaceTakerLogic()
    {
        _numberOfPlaceInArrayPlaces++;

        if (_numberOfPlaceInArrayPlaces == arrayPlaces.Length)
            _numberOfPlaceInArrayPlaces = 0;

        var thisPointVector = arrayPlaces[_numberOfPlaceInArrayPlaces].transform.position;
        transform.forward = thisPointVector - transform.position;
        return thisPointVector;
    }
}