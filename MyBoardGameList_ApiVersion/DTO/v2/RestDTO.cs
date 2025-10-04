using MyBoardGameList.DTO.v1;

namespace MyBoardGameList.DTO.v2
{
    public class RestDTO<T>
    {
        public List<LinkDTO> Links { get; set; } = new List<LinkDTO>();

        public T Items { get; set; } = default!;
    }
}
