namespace App1
{
    class ApplicationModel
    {
        public ApplicationModel()
        {
            OneSettings = new OneSettings();
        }
        private OneSettings _oneSettings;

        public OneSettings OneSettings
        {
            get
            {
                return _oneSettings;
            }

            set
            {
                _oneSettings = value;
            }
        }

    }
}
