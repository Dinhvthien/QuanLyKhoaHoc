namespace QuanLyKhoaHoc.Application.Common.Mappings
{
    public class ProvinceMapping
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;
    }

    public class ProvinceQuery
    {
        public int Page { get; set; }

        public int Size { get; set; }

        public int TotalPage { get; set; }
    }

    public class ProvinceCreate
    {
        public string Name { get; set; } = default!;
    }

    public class ProvinceUpdate
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;
    }
}
