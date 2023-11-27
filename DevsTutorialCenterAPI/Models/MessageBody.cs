namespace DevsTutorialCenterAPI.Models
{
    public static class MessageBody
    {
        public static string Content(string link)
        {

            return $@"
            <div style='padding: 10px;'>
                <h1>Decadev Blog Invite</h1> 
                <p>Congrats! You have been invited to join the Decagon community. Click on the button below to register</p>
                <a type='button' style='color: white; background-color: green; padding: 10px 19px; border-radius: 5px; text-decoration: none;'
                href='{link}'>
                    Accept Invite
                </a>
            </div>
            ";
        }
    }
}
