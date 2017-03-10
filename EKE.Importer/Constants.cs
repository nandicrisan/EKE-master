using System.Collections.Generic;

namespace EKE.Importer
{
    public static class Constants
    {
        public static readonly List<int> ToSkip = new List<int>() { 12, 15, 17, 18, 157, 158, 160, 161, 159, 80, 236 };
        public static readonly List<string> AuthorSearchIn = new List<string>() { "labjegyzet", "bbb_szerzo" };
        public static readonly List<int> Empty = new List<int>() { 74, 72, 73, 37, 38, 39, 40, 41, 42, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 104, 110, 119, 118, 109, 111, 99, 100, 101, 102, 112, 113, 114, 115, 116, 117, 121, 122, 123, 124, 125, 126, 127, 128, 129, 130, 131, 132, 133, 134, 135, 136, 137, 138, 139, 140, 141, 142, 143, 144, 530, 531, 532, 533, 534, 535 };
        public static readonly List<int> Problematic = new List<int>() { 78, 61, 62, 63, 65, 66, 67, 68, 69, 70, 71, 107, 103, 105, 108, 83, 88, 90, 93, 92, 88, 90, 93, 92, 94, 95, 96, 97, 106, 120, 145, 146,412 };
    }
}