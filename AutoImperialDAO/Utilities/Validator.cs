namespace AutoImperialDAO.Utilities
{
    public static class Validator
    {
        /// <summary>
        /// Validates if the id is mora than 0 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static bool IsIdValid(int value)
        {
            if (value <= 0)
            {
                throw new ArgumentException("Cant be 0 or lower");
            }
            return value > 0;
        }
    }
}
