using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UniqueBlog.Domain.Repository;
using UniqueBlog.DTO;
using UniqueBlog.Infrastructure.Log;
using UniqueBlog.Service.Interfaces;
using UniqueBlog.Service.DtoMapper;
using UniqueBlog.Infrastructure.Query;
using UniqueBlog.Domain.Entities;

namespace UniqueBlog.Service
{
    [Export(typeof(IPostCommentService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PostCommentService : IPostCommentService
    {
        private static readonly ILog logger = LoggerFactory.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private IPostCommentRepsoitory _postCommentRepository;

        [ImportingConstructor]
        public PostCommentService(IPostCommentRepsoitory postCommentRepository)
        {
            this._postCommentRepository = postCommentRepository;
        }

        public bool ChangeCommentData(PostCommentDto comment)
        {
            throw new NotImplementedException();
        }

        public bool DeleteComment(int commentId)
        {
            bool flag = false;
            try
            {
                PostComment comment = new PostComment(commentId);
                this._postCommentRepository.Remove(comment);
                flag = true;
            }
            catch (Exception ex)
            {
                logger.Error("There is an error while add a comment", ex);
            }

            return flag;
        }

        public IEnumerable<PostCommentDto> GetPostCommentListByPostId(int postId)
        {
            Query query = new Query();
            query.Add(new Criterion("PostId", postId, CriterionOperator.Equal));

            IList<PostCommentDto> postCommentDtoList = new List<PostCommentDto>();

            try
            {
                var postCommentList = this._postCommentRepository.FindBy(query);

                
                foreach (var comment in postCommentList)
                {
                    postCommentDtoList.Add(comment.ConvertTo());
                }
            }
            catch (Exception ex)
            {
                logger.Error("There is an error while query the comment list", ex);
            }

            return postCommentDtoList;
        }

        public bool PublishComment(PostCommentDto comment)
        {
            var postComment = comment.ConvertTo();
            bool flag = false;
            try
            {
                this._postCommentRepository.Add(postComment);
                flag = true;
            }
          
            catch(Exception ex)
            {
                logger.Error("There is an error while add a comment", ex);
            }

            return flag;
        }
    }
}
