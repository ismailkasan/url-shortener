using FluentValidation;

namespace URLShortener.Common
{
    public class UrlRequestDto
    {
        public UrlRequestDto(string longUrl, string customSegment)
        {
            LongUrl = longUrl;
            CustomSegment = customSegment;
        }
        public string LongUrl { get; set; }
        public string CustomSegment { get; set; }
        public bool? IsCustomUrl { get; set; }
    }

    public class UrlRequestDtoValidator : AbstractValidator<UrlRequestDto>
    {
        public UrlRequestDtoValidator()
        {
            RuleFor(x => x.LongUrl).NotEmpty().NotNull().WithMessage("Url must be not empty!");

            When(x => x.IsCustomUrl.HasValue && x.IsCustomUrl == true, () =>
            {
                RuleFor(x => x.CustomSegment).NotEmpty().NotNull().WithMessage("Segment must be not empty!");
                RuleFor(x => x.CustomSegment).MaximumLength(Constant.ShortLinkSegmentLength).WithMessage($"Custom url segment maximum length can be {Constant.ShortLinkSegmentLength}!");
            });
        }
    }
}
