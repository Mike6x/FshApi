namespace FSH.WebApi.Application.Property.AssetHistorys;

public class AssetHistoryDto : IDto
{
    public DefaultIdType Id { get; set; }
    public DefaultIdType AssetId { get; set; }
    public string AssetName { get; set; } = default!;

    public DefaultIdType? PreviousQualityStatusId { get; set; }
    public string? PreviousQualityStatusName { get; set; }
    public DefaultIdType QualityStatusId { get; set; }
    public string QualityStatusName { get; set; } = default!;

    public DefaultIdType? PreviousUsingStatusId { get; set; }
    public string? PreviousUsingStatusName { get; set; }
    public DefaultIdType UsingStatusId { get; set; }
    public string UsingStatusName { get; set; } = default!;

    public DefaultIdType? EmployeeId { get; set; }
    public string? EmployeeFirstName { get; set; }
    public string? EmployeeLastName { get; set; }

    public string? DoccumentPath { get; set; }
    public string? DoccumentLink { get; set; }
    public string? Note { get; set; }

    public DefaultIdType LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
}

public class AssetHistoryDetailsDto : IDto
{
    public DefaultIdType Id { get; set; }
    public DefaultIdType AssetId { get; set; }
    public string AssetName { get; set; } = default!;

    public DefaultIdType UsingStatusId { get; set; }
    public string UsingStatusName { get; set; } = default!;
    public DefaultIdType QualityStatusId { get; set; }
    public string QualityStatusName { get; set; } = default!;
}

public class AssetHistoryExportDto : IDto
{
    public DefaultIdType Id { get; set; }

    public DefaultIdType AssetId { get; set; }
    public string AssetName { get; set; } = default!;

    public DefaultIdType? PreviousQualityStatusId { get; set; }
    public string? PreviousQualityStatusName { get; set; }
    public DefaultIdType QualityStatusId { get; set; }
    public string QualityStatusName { get; set; } = default!;

    public DefaultIdType? PreviousUsingStatusId { get; set; }
    public string? PreviousUsingStatusName { get; set; }
    public DefaultIdType UsingStatusId { get; set; }
    public string UsingStatusName { get; set; } = default!;

    public DefaultIdType? EmployeeId { get; set; }
    public string? EmployeeFirstName { get; set; }
    public string? EmployeeLastName { get; set; }

    public string? DoccumentPath { get; set; }
    public string? DoccumentLink { get; set; }
    public string? Note { get; set; }

    public DefaultIdType CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public DefaultIdType LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
}