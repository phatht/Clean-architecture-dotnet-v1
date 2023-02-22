namespace mcr_service_webhooks.Domain.Models
{
    public enum HookResourceAction
    {
        undefined,
        hook_created,
        hook_removed,
        hook_updated,
        // etc...
    }

    // Actions of ProjectEventType
    public enum projectAction
    {
        undefined,
        project_created,
        project_renamed,
        project_archived,
        //etc...
    }
}
