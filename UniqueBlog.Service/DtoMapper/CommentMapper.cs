using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniqueBlog.DTO;
using UniqueBlog.Domain.Entities;

namespace UniqueBlog.Service.DtoMapper
{
    public static class CommentMapper
    {
        public static PostCommentDto ConvertTo(this PostComment comment)
        {
            return DtoMapper.AutoMapperConfig.MapperInstance.Map<PostComment, PostCommentDto>(comment);
        }

        public static PostCommentDto ConvertTo(this PostComment comment, bool ignoreNavigationProperties)
        {
            if (!ignoreNavigationProperties)
            {
                return comment.ConvertTo();
            }
            else
            {
                return DtoMapper.AutoMapperConfig.MapperInstance.Map<PostComment, PostCommentDto>(comment, opt =>
                {
                    comment.Post = null;
                });
            }
        }


        public static PostComment ConvertTo(this PostCommentDto commentDto)
        {
            return DtoMapper.AutoMapperConfig.MapperInstance.Map<PostCommentDto, PostComment>(commentDto);
        }
    }
}
