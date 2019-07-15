using System.Runtime.CompilerServices;
using System.Text;
[assembly: InternalsVisibleTo("MapTravel.Test")]
namespace MapTravel.BL
{
    public class MapDirections
    {

        private const int PositionVisited = 1;
        private readonly char[,] _charMap;
        private readonly int[,] _currentPostitionArray;
        private readonly int _rows;
        private readonly int _columns;
        private int _currentRowIndex;
        private int _currentColumnIndex;

        public MapDirections(char[,] charMap, int rows, int columns, int startRowIndex, int startColumnIndex)
        {
            _charMap = charMap;
            _currentPostitionArray = new int[rows, columns];
            _currentRowIndex = startRowIndex;
            _currentColumnIndex = startColumnIndex;
            _rows = rows;
            _columns = columns;
        }

        public bool CurrentPosition
        {
            get { return _currentPostitionArray[_currentRowIndex, _currentColumnIndex] == PositionVisited; }
        }

        public char CurrentElement
        {
            get { return _charMap[_currentRowIndex, _currentColumnIndex]; }
        }
        /// <summary>
        /// Adjust current indexes and remember position
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public bool AdjustIndexes(MapMove direction)
        {
            if (!MoveToNextElement(direction, out int nextRow, out int nextColumn))
                return false;
            
            _currentPostitionArray[_currentRowIndex, _currentColumnIndex] = PositionVisited;

            _currentRowIndex = nextRow;
            _currentColumnIndex = nextColumn;

            return true;
        }

        /// <summary>
        /// Determine if we can move to this element
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public bool IsValidMove(MapMove direction)
        {
            var element = SearchNextChar(direction);
            return element.HasValue && element != MapConstant.Blank;
        }
        /// <summary>
        /// Return next character
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public char? SearchNextChar(MapMove direction)
        {
            char? element = null;

            if (MoveToNextElement(direction, out int nextRow, out int nextColumn))
                element = _charMap[nextRow, nextColumn];

            return element;
        }
        /// <summary>
        /// Tell us next row and column index
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="nextRow"></param>
        /// <param name="nextColumn"></param>
        /// <returns></returns>
        private bool MoveToNextElement(MapMove direction, out int nextRow, out int nextColumn)
        {
            nextRow = _currentRowIndex;
            nextColumn = _currentColumnIndex;

            switch (direction)
            {
                case MapMove.Up:
                    nextRow--;
                    break;
                case MapMove.Down:
                    nextRow++;
                    break;
                case MapMove.Left:
                    nextColumn--;
                    break;
                case MapMove.Right:
                    nextColumn++;
                    break;
                case MapMove.None:
                default:
                    break;
            }
            // validate result logic
            if (nextRow < 0 || nextRow >= _rows || nextColumn < 0 || nextColumn >= _columns)
                return false;

            return true;
        }



        public static MapTravelResult SearchMap(string asciiMap)
        {
            return CreateMapResult(MapGenerator.GenerateMap(asciiMap));
        }
        /// <summary>
        /// Iterate until end character is found and build map
        /// </summary>
        /// <param name="mapBoard"></param>
        /// <returns></returns>
        private static MapTravelResult CreateMapResult(MapDirections mapBoard)
        {
            var letters = new StringBuilder();
            var characterPath = new StringBuilder();

            var currentDirection = MapMove.None;

            while (true)
            {
                var currentElement = mapBoard.CurrentElement;
                characterPath.Append(currentElement);

                if (currentElement == MapConstant.End)
                    break;

                if (char.IsLetter(currentElement) && !mapBoard.CurrentPosition)
                    letters.Append(currentElement);

                currentDirection = FindNextMove(currentDirection, mapBoard, characterPath);

                if (currentDirection == MapMove.None)
                {
                    string exceptionMessage = "Path is invalid at character: " + characterPath.ToString();
                    throw new MapException(exceptionMessage);
                }


                mapBoard.AdjustIndexes(currentDirection);
            }

            return new MapTravelResult(letters.ToString(), characterPath.ToString());
        }

        /// <summary>
        /// Determine next direction
        /// </summary>
        /// <param name="currentDirection"></param>
        /// <param name="mapBoard"></param>
        /// <param name="characterPath"></param>
        /// <returns></returns>
        private static MapMove FindNextMove(MapMove currentDirection, MapDirections mapBoard, StringBuilder characterPath)
        {
            var direction = currentDirection;

            if (currentDirection == MapMove.None)
            {
                direction = GetDirection(MapMove.Up, MapMove.Right, mapBoard, characterPath);
                direction = GetDirection(direction, MapMove.Down, mapBoard, characterPath);
                direction = GetDirection(direction, MapMove.Left, mapBoard, characterPath);
            }
            else if (!mapBoard.IsValidMove(currentDirection))
            {
                // if current position is up or down go left or right and vice versa
                if (currentDirection == MapMove.Up || currentDirection == MapMove.Down)
                    direction = GetDirection(MapMove.Left, MapMove.Right, mapBoard, characterPath);
                else if (currentDirection == MapMove.Left || currentDirection == MapMove.Right)
                    direction = GetDirection(MapMove.Up, MapMove.Down, mapBoard, characterPath);
            }

            return direction;
        }
        /// <summary>
        /// Based on two input directions determine next move
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="mapBoard"></param>
        /// <param name="characterPath"></param>
        /// <returns></returns>
        private static MapMove GetDirection(MapMove first, MapMove second, MapDirections mapBoard, StringBuilder characterPath)
        {
            var takeFirst = first != MapMove.None && mapBoard.IsValidMove(first);
            var takeSecond = second != MapMove.None && mapBoard.IsValidMove(second);

            var firstDirectionMark = GetDirectionMove(first, mapBoard.SearchNextChar(first));
            var secondDirectionMark = GetDirectionMove(second, mapBoard.SearchNextChar(second));
            // first check for possible invalid moves
            // only one direction is valid
            if (takeFirst && firstDirectionMark && takeSecond && secondDirectionMark)
            {
                string exceptionMessage = "Path is invalid at character: " + characterPath.ToString();
                throw new MapException(exceptionMessage);
            }
            // only one direction is valid
            else if (takeFirst && takeSecond)
            {
                string exceptionMessage = "Path is invalid at character: " + characterPath.ToString();
                throw new MapException(exceptionMessage);
            }
            // take valid move
            else if (takeFirst && firstDirectionMark)
                return first;
            else if (takeSecond && secondDirectionMark)
                return second;
            else if (takeFirst)
                return first;
            else if (takeSecond)
                return second;
            else
                return MapMove.None;
        }
        /// <summary>
        /// Determine if next character is mark for direction (left, right, up, down)
        /// </summary>
        /// <param name="currentMove"></param>
        /// <param name="nextChar"></param>
        /// <returns></returns>
        private static bool GetDirectionMove(MapMove currentMove, char? nextChar)
        {
            if (!nextChar.HasValue)
                return false;

            if (currentMove == MapMove.Left || currentMove == MapMove.Right)
                return nextChar == MapConstant.FromLeftToRight;
            else if (currentMove == MapMove.Up || currentMove == MapMove.Down)
                return nextChar == MapConstant.UpToDown;
            else
                return false;
        }



    }
}
