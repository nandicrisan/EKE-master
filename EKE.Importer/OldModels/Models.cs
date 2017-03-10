namespace EKE.Importer.OldModels
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Models : DbContext
    {
        public Models()
            : base("name=Models")
        {
        }

        public virtual DbSet<jos_ak_profiles> jos_ak_profiles { get; set; }
        public virtual DbSet<jos_ak_stats> jos_ak_stats { get; set; }
        public virtual DbSet<jos_banner> jos_banner { get; set; }
        public virtual DbSet<jos_bannerclient> jos_bannerclient { get; set; }
        public virtual DbSet<jos_categories> jos_categories { get; set; }
        public virtual DbSet<jos_components> jos_components { get; set; }
        public virtual DbSet<jos_contact_details> jos_contact_details { get; set; }
        public virtual DbSet<jos_content> jos_content { get; set; }
        public virtual DbSet<jos_content_frontpage> jos_content_frontpage { get; set; }
        public virtual DbSet<jos_content_rating> jos_content_rating { get; set; }
        public virtual DbSet<jos_core_acl_aro> jos_core_acl_aro { get; set; }
        public virtual DbSet<jos_core_acl_aro_groups> jos_core_acl_aro_groups { get; set; }
        public virtual DbSet<jos_core_acl_aro_map> jos_core_acl_aro_map { get; set; }
        public virtual DbSet<jos_core_acl_aro_sections> jos_core_acl_aro_sections { get; set; }
        public virtual DbSet<jos_em_mysqladmin_log> jos_em_mysqladmin_log { get; set; }
        public virtual DbSet<jos_groups> jos_groups { get; set; }
        public virtual DbSet<jos_jlord_todo> jos_jlord_todo { get; set; }
        public virtual DbSet<jos_joomlawatch> jos_joomlawatch { get; set; }
        public virtual DbSet<jos_joomlawatch_blocked> jos_joomlawatch_blocked { get; set; }
        public virtual DbSet<jos_joomlawatch_cache> jos_joomlawatch_cache { get; set; }
        public virtual DbSet<jos_joomlawatch_cc2c> jos_joomlawatch_cc2c { get; set; }
        public virtual DbSet<jos_joomlawatch_config> jos_joomlawatch_config { get; set; }
        public virtual DbSet<jos_joomlawatch_goals> jos_joomlawatch_goals { get; set; }
        public virtual DbSet<jos_joomlawatch_info> jos_joomlawatch_info { get; set; }
        public virtual DbSet<jos_joomlawatch_internal> jos_joomlawatch_internal { get; set; }
        public virtual DbSet<jos_joomlawatch_ip2c> jos_joomlawatch_ip2c { get; set; }
        public virtual DbSet<jos_joomlawatch_uri> jos_joomlawatch_uri { get; set; }
        public virtual DbSet<jos_joomlawatch_uri2title> jos_joomlawatch_uri2title { get; set; }
        public virtual DbSet<jos_jp_dbtf> jos_jp_dbtf { get; set; }
        public virtual DbSet<jos_jp_def> jos_jp_def { get; set; }
        public virtual DbSet<jos_jp_exclusion> jos_jp_exclusion { get; set; }
        public virtual DbSet<jos_jp_extradb> jos_jp_extradb { get; set; }
        public virtual DbSet<jos_jp_inclusion> jos_jp_inclusion { get; set; }
        public virtual DbSet<jos_jp_packvars> jos_jp_packvars { get; set; }
        public virtual DbSet<jos_jp_sff> jos_jp_sff { get; set; }
        public virtual DbSet<jos_linkr_bookmarks> jos_linkr_bookmarks { get; set; }
        public virtual DbSet<jos_menu> jos_menu { get; set; }
        public virtual DbSet<jos_menu_types> jos_menu_types { get; set; }
        public virtual DbSet<jos_messages> jos_messages { get; set; }
        public virtual DbSet<jos_migration_backlinks> jos_migration_backlinks { get; set; }
        public virtual DbSet<jos_modules> jos_modules { get; set; }
        public virtual DbSet<jos_modules_menu> jos_modules_menu { get; set; }
        public virtual DbSet<jos_newsfeeds> jos_newsfeeds { get; set; }
        public virtual DbSet<jos_phocadownload> jos_phocadownload { get; set; }
        public virtual DbSet<jos_phocadownload_categories> jos_phocadownload_categories { get; set; }
        public virtual DbSet<jos_phocadownload_licenses> jos_phocadownload_licenses { get; set; }
        public virtual DbSet<jos_phocadownload_sections> jos_phocadownload_sections { get; set; }
        public virtual DbSet<jos_phocadownload_settings> jos_phocadownload_settings { get; set; }
        public virtual DbSet<jos_phocadownload_user_stat> jos_phocadownload_user_stat { get; set; }
        public virtual DbSet<jos_plugins> jos_plugins { get; set; }
        public virtual DbSet<jos_poll_data> jos_poll_data { get; set; }
        public virtual DbSet<jos_poll_date> jos_poll_date { get; set; }
        public virtual DbSet<jos_poll_menu> jos_poll_menu { get; set; }
        public virtual DbSet<jos_polls> jos_polls { get; set; }
        public virtual DbSet<jos_rokdownloads> jos_rokdownloads { get; set; }
        public virtual DbSet<jos_sections> jos_sections { get; set; }
        public virtual DbSet<jos_sefexts> jos_sefexts { get; set; }
        public virtual DbSet<jos_sefexttexts> jos_sefexttexts { get; set; }
        public virtual DbSet<jos_sefmoved> jos_sefmoved { get; set; }
        public virtual DbSet<jos_sefurls> jos_sefurls { get; set; }
        public virtual DbSet<jos_sefurlword_xref> jos_sefurlword_xref { get; set; }
        public virtual DbSet<jos_sefwords> jos_sefwords { get; set; }
        public virtual DbSet<jos_session> jos_session { get; set; }
        public virtual DbSet<jos_tag_category_map> jos_tag_category_map { get; set; }
        public virtual DbSet<jos_tag_hideshow> jos_tag_hideshow { get; set; }
        public virtual DbSet<jos_tag_layouts> jos_tag_layouts { get; set; }
        public virtual DbSet<jos_tag_tags> jos_tag_tags { get; set; }
        public virtual DbSet<jos_templates_menu> jos_templates_menu { get; set; }
        public virtual DbSet<jos_users> jos_users { get; set; }
        public virtual DbSet<jos_weblinks> jos_weblinks { get; set; }
        public virtual DbSet<jos_wf_profiles> jos_wf_profiles { get; set; }
        public virtual DbSet<jos_xmap> jos_xmap { get; set; }
        public virtual DbSet<jos_xmap_ext> jos_xmap_ext { get; set; }
        public virtual DbSet<jos_xmap_items> jos_xmap_items { get; set; }
        public virtual DbSet<jos_xmap_sitemap> jos_xmap_sitemap { get; set; }
        public virtual DbSet<jos_yvcomment> jos_yvcomment { get; set; }
        public virtual DbSet<jos_bannertrack> jos_bannertrack { get; set; }
        public virtual DbSet<jos_bfdbversions> jos_bfdbversions { get; set; }
        public virtual DbSet<jos_core_acl_groups_aro_map> jos_core_acl_groups_aro_map { get; set; }
        public virtual DbSet<jos_core_log_items> jos_core_log_items { get; set; }
        public virtual DbSet<jos_core_log_searches> jos_core_log_searches { get; set; }
        public virtual DbSet<jos_messages_cfg> jos_messages_cfg { get; set; }
        public virtual DbSet<jos_rokversions> jos_rokversions { get; set; }
        public virtual DbSet<jos_stats_agents> jos_stats_agents { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<jos_ak_profiles>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<jos_ak_profiles>()
                .Property(e => e.configuration)
                .IsUnicode(false);

            modelBuilder.Entity<jos_ak_profiles>()
                .Property(e => e.filters)
                .IsUnicode(false);

            modelBuilder.Entity<jos_ak_stats>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<jos_ak_stats>()
                .Property(e => e.comment)
                .IsUnicode(false);

            modelBuilder.Entity<jos_ak_stats>()
                .Property(e => e.status)
                .IsUnicode(false);

            modelBuilder.Entity<jos_ak_stats>()
                .Property(e => e.origin)
                .IsUnicode(false);

            modelBuilder.Entity<jos_ak_stats>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<jos_ak_stats>()
                .Property(e => e.archivename)
                .IsUnicode(false);

            modelBuilder.Entity<jos_ak_stats>()
                .Property(e => e.absolute_path)
                .IsUnicode(false);

            modelBuilder.Entity<jos_banner>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<jos_banner>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<jos_banner>()
                .Property(e => e.alias)
                .IsUnicode(false);

            modelBuilder.Entity<jos_banner>()
                .Property(e => e.imageurl)
                .IsUnicode(false);

            modelBuilder.Entity<jos_banner>()
                .Property(e => e.clickurl)
                .IsUnicode(false);

            modelBuilder.Entity<jos_banner>()
                .Property(e => e.editor)
                .IsUnicode(false);

            modelBuilder.Entity<jos_banner>()
                .Property(e => e.custombannercode)
                .IsUnicode(false);

            modelBuilder.Entity<jos_banner>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<jos_banner>()
                .Property(e => e.tags)
                .IsUnicode(false);

            modelBuilder.Entity<jos_banner>()
                .Property(e => e.name_params)
                .IsUnicode(false);

            modelBuilder.Entity<jos_bannerclient>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<jos_bannerclient>()
                .Property(e => e.contact)
                .IsUnicode(false);

            modelBuilder.Entity<jos_bannerclient>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<jos_bannerclient>()
                .Property(e => e.extrainfo)
                .IsUnicode(false);

            modelBuilder.Entity<jos_bannerclient>()
                .Property(e => e.editor)
                .IsUnicode(false);

            modelBuilder.Entity<jos_categories>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<jos_categories>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<jos_categories>()
                .Property(e => e.alias)
                .IsUnicode(false);

            modelBuilder.Entity<jos_categories>()
                .Property(e => e.image)
                .IsUnicode(false);

            modelBuilder.Entity<jos_categories>()
                .Property(e => e.section)
                .IsUnicode(false);

            modelBuilder.Entity<jos_categories>()
                .Property(e => e.image_position)
                .IsUnicode(false);

            modelBuilder.Entity<jos_categories>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<jos_categories>()
                .Property(e => e.editor)
                .IsUnicode(false);

            modelBuilder.Entity<jos_categories>()
                .Property(e => e.name_params)
                .IsUnicode(false);

            modelBuilder.Entity<jos_components>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<jos_components>()
                .Property(e => e.link)
                .IsUnicode(false);

            modelBuilder.Entity<jos_components>()
                .Property(e => e.admin_menu_link)
                .IsUnicode(false);

            modelBuilder.Entity<jos_components>()
                .Property(e => e.admin_menu_alt)
                .IsUnicode(false);

            modelBuilder.Entity<jos_components>()
                .Property(e => e.option)
                .IsUnicode(false);

            modelBuilder.Entity<jos_components>()
                .Property(e => e.admin_menu_img)
                .IsUnicode(false);

            modelBuilder.Entity<jos_components>()
                .Property(e => e.name_params)
                .IsUnicode(false);

            modelBuilder.Entity<jos_contact_details>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<jos_contact_details>()
                .Property(e => e.alias)
                .IsUnicode(false);

            modelBuilder.Entity<jos_contact_details>()
                .Property(e => e.con_position)
                .IsUnicode(false);

            modelBuilder.Entity<jos_contact_details>()
                .Property(e => e.address)
                .IsUnicode(false);

            modelBuilder.Entity<jos_contact_details>()
                .Property(e => e.suburb)
                .IsUnicode(false);

            modelBuilder.Entity<jos_contact_details>()
                .Property(e => e.state)
                .IsUnicode(false);

            modelBuilder.Entity<jos_contact_details>()
                .Property(e => e.country)
                .IsUnicode(false);

            modelBuilder.Entity<jos_contact_details>()
                .Property(e => e.postcode)
                .IsUnicode(false);

            modelBuilder.Entity<jos_contact_details>()
                .Property(e => e.telephone)
                .IsUnicode(false);

            modelBuilder.Entity<jos_contact_details>()
                .Property(e => e.fax)
                .IsUnicode(false);

            modelBuilder.Entity<jos_contact_details>()
                .Property(e => e.misc)
                .IsUnicode(false);

            modelBuilder.Entity<jos_contact_details>()
                .Property(e => e.image)
                .IsUnicode(false);

            modelBuilder.Entity<jos_contact_details>()
                .Property(e => e.imagepos)
                .IsUnicode(false);

            modelBuilder.Entity<jos_contact_details>()
                .Property(e => e.email_to)
                .IsUnicode(false);

            modelBuilder.Entity<jos_contact_details>()
                .Property(e => e.name_params)
                .IsUnicode(false);

            modelBuilder.Entity<jos_contact_details>()
                .Property(e => e.mobile)
                .IsUnicode(false);

            modelBuilder.Entity<jos_contact_details>()
                .Property(e => e.webpage)
                .IsUnicode(false);

            modelBuilder.Entity<jos_content>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<jos_content>()
                .Property(e => e.alias)
                .IsUnicode(false);

            modelBuilder.Entity<jos_content>()
                .Property(e => e.title_alias)
                .IsUnicode(false);

            modelBuilder.Entity<jos_content>()
                .Property(e => e.introtext)
                .IsUnicode(false);

            modelBuilder.Entity<jos_content>()
                .Property(e => e.fulltext)
                .IsUnicode(false);

            modelBuilder.Entity<jos_content>()
                .Property(e => e.created_by_alias)
                .IsUnicode(false);

            modelBuilder.Entity<jos_content>()
                .Property(e => e.images)
                .IsUnicode(false);

            modelBuilder.Entity<jos_content>()
                .Property(e => e.urls)
                .IsUnicode(false);

            modelBuilder.Entity<jos_content>()
                .Property(e => e.attribs)
                .IsUnicode(false);

            modelBuilder.Entity<jos_content>()
                .Property(e => e.metakey)
                .IsUnicode(false);

            modelBuilder.Entity<jos_content>()
                .Property(e => e.metadesc)
                .IsUnicode(false);

            modelBuilder.Entity<jos_content>()
                .Property(e => e.metadata)
                .IsUnicode(false);

            modelBuilder.Entity<jos_content_rating>()
                .Property(e => e.lastip)
                .IsUnicode(false);

            modelBuilder.Entity<jos_core_acl_aro>()
                .Property(e => e.section_value)
                .IsUnicode(false);

            modelBuilder.Entity<jos_core_acl_aro>()
                .Property(e => e.value)
                .IsUnicode(false);

            modelBuilder.Entity<jos_core_acl_aro>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<jos_core_acl_aro_groups>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<jos_core_acl_aro_groups>()
                .Property(e => e.value)
                .IsUnicode(false);

            modelBuilder.Entity<jos_core_acl_aro_map>()
                .Property(e => e.section_value)
                .IsUnicode(false);

            modelBuilder.Entity<jos_core_acl_aro_map>()
                .Property(e => e.value)
                .IsUnicode(false);

            modelBuilder.Entity<jos_core_acl_aro_sections>()
                .Property(e => e.value)
                .IsUnicode(false);

            modelBuilder.Entity<jos_core_acl_aro_sections>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<jos_groups>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<jos_jlord_todo>()
                .Property(e => e.message)
                .IsUnicode(false);

            modelBuilder.Entity<jos_joomlawatch>()
                .Property(e => e.ip)
                .IsUnicode(false);

            modelBuilder.Entity<jos_joomlawatch>()
                .Property(e => e.country)
                .IsUnicode(false);

            modelBuilder.Entity<jos_joomlawatch>()
                .Property(e => e.browser)
                .IsUnicode(false);

            modelBuilder.Entity<jos_joomlawatch>()
                .Property(e => e.referer)
                .IsUnicode(false);

            modelBuilder.Entity<jos_joomlawatch>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<jos_joomlawatch_blocked>()
                .Property(e => e.ip)
                .IsUnicode(false);

            modelBuilder.Entity<jos_joomlawatch_blocked>()
                .Property(e => e.reason)
                .IsUnicode(false);

            modelBuilder.Entity<jos_joomlawatch_cache>()
                .Property(e => e.key)
                .IsUnicode(false);

            modelBuilder.Entity<jos_joomlawatch_cache>()
                .Property(e => e.cache)
                .IsUnicode(false);

            modelBuilder.Entity<jos_joomlawatch_cc2c>()
                .Property(e => e.cc)
                .IsUnicode(false);

            modelBuilder.Entity<jos_joomlawatch_cc2c>()
                .Property(e => e.country)
                .IsUnicode(false);

            modelBuilder.Entity<jos_joomlawatch_config>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<jos_joomlawatch_config>()
                .Property(e => e.value)
                .IsUnicode(false);

            modelBuilder.Entity<jos_joomlawatch_goals>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<jos_joomlawatch_goals>()
                .Property(e => e.uri_condition)
                .IsUnicode(false);

            modelBuilder.Entity<jos_joomlawatch_goals>()
                .Property(e => e.get_var)
                .IsUnicode(false);

            modelBuilder.Entity<jos_joomlawatch_goals>()
                .Property(e => e.get_condition)
                .IsUnicode(false);

            modelBuilder.Entity<jos_joomlawatch_goals>()
                .Property(e => e.post_var)
                .IsUnicode(false);

            modelBuilder.Entity<jos_joomlawatch_goals>()
                .Property(e => e.post_condition)
                .IsUnicode(false);

            modelBuilder.Entity<jos_joomlawatch_goals>()
                .Property(e => e.title_condition)
                .IsUnicode(false);

            modelBuilder.Entity<jos_joomlawatch_goals>()
                .Property(e => e.username_condition)
                .IsUnicode(false);

            modelBuilder.Entity<jos_joomlawatch_goals>()
                .Property(e => e.ip_condition)
                .IsUnicode(false);

            modelBuilder.Entity<jos_joomlawatch_goals>()
                .Property(e => e.came_from_condition)
                .IsUnicode(false);

            modelBuilder.Entity<jos_joomlawatch_goals>()
                .Property(e => e.country_condition)
                .IsUnicode(false);

            modelBuilder.Entity<jos_joomlawatch_goals>()
                .Property(e => e.block)
                .IsUnicode(false);

            modelBuilder.Entity<jos_joomlawatch_goals>()
                .Property(e => e.redirect)
                .IsUnicode(false);

            modelBuilder.Entity<jos_joomlawatch_info>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<jos_joomlawatch_internal>()
                .Property(e => e.from)
                .IsUnicode(false);

            modelBuilder.Entity<jos_joomlawatch_internal>()
                .Property(e => e.to)
                .IsUnicode(false);

            modelBuilder.Entity<jos_joomlawatch_ip2c>()
                .Property(e => e.country)
                .IsUnicode(false);

            modelBuilder.Entity<jos_joomlawatch_uri>()
                .Property(e => e.uri)
                .IsUnicode(false);

            modelBuilder.Entity<jos_joomlawatch_uri>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<jos_joomlawatch_uri2title>()
                .Property(e => e.uri)
                .IsUnicode(false);

            modelBuilder.Entity<jos_joomlawatch_uri2title>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<jos_jp_dbtf>()
                .Property(e => e.tablename)
                .IsUnicode(false);

            modelBuilder.Entity<jos_jp_def>()
                .Property(e => e.directory)
                .IsUnicode(false);

            modelBuilder.Entity<jos_jp_exclusion>()
                .Property(e => e.name_class)
                .IsUnicode(false);

            modelBuilder.Entity<jos_jp_exclusion>()
                .Property(e => e.value)
                .IsUnicode(false);

            modelBuilder.Entity<jos_jp_extradb>()
                .Property(e => e.host)
                .IsUnicode(false);

            modelBuilder.Entity<jos_jp_extradb>()
                .Property(e => e.port)
                .IsUnicode(false);

            modelBuilder.Entity<jos_jp_extradb>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<jos_jp_extradb>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<jos_jp_extradb>()
                .Property(e => e.database)
                .IsUnicode(false);

            modelBuilder.Entity<jos_jp_inclusion>()
                .Property(e => e.name_class)
                .IsUnicode(false);

            modelBuilder.Entity<jos_jp_inclusion>()
                .Property(e => e.value)
                .IsUnicode(false);

            modelBuilder.Entity<jos_jp_packvars>()
                .Property(e => e.key)
                .IsUnicode(false);

            modelBuilder.Entity<jos_jp_packvars>()
                .Property(e => e.value)
                .IsUnicode(false);

            modelBuilder.Entity<jos_jp_packvars>()
                .Property(e => e.value2)
                .IsUnicode(false);

            modelBuilder.Entity<jos_jp_sff>()
                .Property(e => e.file)
                .IsUnicode(false);

            modelBuilder.Entity<jos_linkr_bookmarks>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<jos_linkr_bookmarks>()
                .Property(e => e.text)
                .IsUnicode(false);

            modelBuilder.Entity<jos_linkr_bookmarks>()
                .Property(e => e.size)
                .IsUnicode(false);

            modelBuilder.Entity<jos_linkr_bookmarks>()
                .Property(e => e.htmltext)
                .IsUnicode(false);

            modelBuilder.Entity<jos_linkr_bookmarks>()
                .Property(e => e.htmlsmall)
                .IsUnicode(false);

            modelBuilder.Entity<jos_linkr_bookmarks>()
                .Property(e => e.htmllarge)
                .IsUnicode(false);

            modelBuilder.Entity<jos_linkr_bookmarks>()
                .Property(e => e.htmlbutton)
                .IsUnicode(false);

            modelBuilder.Entity<jos_linkr_bookmarks>()
                .Property(e => e.htmlcustom)
                .IsUnicode(false);

            modelBuilder.Entity<jos_linkr_bookmarks>()
                .Property(e => e.icon)
                .IsUnicode(false);

            modelBuilder.Entity<jos_menu>()
                .Property(e => e.menutype)
                .IsUnicode(false);

            modelBuilder.Entity<jos_menu>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<jos_menu>()
                .Property(e => e.alias)
                .IsUnicode(false);

            modelBuilder.Entity<jos_menu>()
                .Property(e => e.link)
                .IsUnicode(false);

            modelBuilder.Entity<jos_menu>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<jos_menu>()
                .Property(e => e.name_params)
                .IsUnicode(false);

            modelBuilder.Entity<jos_menu_types>()
                .Property(e => e.menutype)
                .IsUnicode(false);

            modelBuilder.Entity<jos_menu_types>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<jos_menu_types>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<jos_messages>()
                .Property(e => e.subject)
                .IsUnicode(false);

            modelBuilder.Entity<jos_messages>()
                .Property(e => e.message)
                .IsUnicode(false);

            modelBuilder.Entity<jos_migration_backlinks>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<jos_migration_backlinks>()
                .Property(e => e.url)
                .IsUnicode(false);

            modelBuilder.Entity<jos_migration_backlinks>()
                .Property(e => e.sefurl)
                .IsUnicode(false);

            modelBuilder.Entity<jos_migration_backlinks>()
                .Property(e => e.newurl)
                .IsUnicode(false);

            modelBuilder.Entity<jos_modules>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<jos_modules>()
                .Property(e => e.content)
                .IsUnicode(false);

            modelBuilder.Entity<jos_modules>()
                .Property(e => e.position)
                .IsUnicode(false);

            modelBuilder.Entity<jos_modules>()
                .Property(e => e.module)
                .IsUnicode(false);

            modelBuilder.Entity<jos_modules>()
                .Property(e => e.name_params)
                .IsUnicode(false);

            modelBuilder.Entity<jos_modules>()
                .Property(e => e.control)
                .IsUnicode(false);

            modelBuilder.Entity<jos_newsfeeds>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<jos_newsfeeds>()
                .Property(e => e.alias)
                .IsUnicode(false);

            modelBuilder.Entity<jos_newsfeeds>()
                .Property(e => e.link)
                .IsUnicode(false);

            modelBuilder.Entity<jos_newsfeeds>()
                .Property(e => e.filename)
                .IsUnicode(false);

            modelBuilder.Entity<jos_phocadownload>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<jos_phocadownload>()
                .Property(e => e.alias)
                .IsUnicode(false);

            modelBuilder.Entity<jos_phocadownload>()
                .Property(e => e.filename)
                .IsUnicode(false);

            modelBuilder.Entity<jos_phocadownload>()
                .Property(e => e.filename_play)
                .IsUnicode(false);

            modelBuilder.Entity<jos_phocadownload>()
                .Property(e => e.filename_preview)
                .IsUnicode(false);

            modelBuilder.Entity<jos_phocadownload>()
                .Property(e => e.author)
                .IsUnicode(false);

            modelBuilder.Entity<jos_phocadownload>()
                .Property(e => e.author_email)
                .IsUnicode(false);

            modelBuilder.Entity<jos_phocadownload>()
                .Property(e => e.author_url)
                .IsUnicode(false);

            modelBuilder.Entity<jos_phocadownload>()
                .Property(e => e.license)
                .IsUnicode(false);

            modelBuilder.Entity<jos_phocadownload>()
                .Property(e => e.license_url)
                .IsUnicode(false);

            modelBuilder.Entity<jos_phocadownload>()
                .Property(e => e.image_filename)
                .IsUnicode(false);

            modelBuilder.Entity<jos_phocadownload>()
                .Property(e => e.image_filename_spec1)
                .IsUnicode(false);

            modelBuilder.Entity<jos_phocadownload>()
                .Property(e => e.image_filename_spec2)
                .IsUnicode(false);

            modelBuilder.Entity<jos_phocadownload>()
                .Property(e => e.image_download)
                .IsUnicode(false);

            modelBuilder.Entity<jos_phocadownload>()
                .Property(e => e.link_external)
                .IsUnicode(false);

            modelBuilder.Entity<jos_phocadownload>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<jos_phocadownload>()
                .Property(e => e.version)
                .IsUnicode(false);

            modelBuilder.Entity<jos_phocadownload>()
                .Property(e => e.name_params)
                .IsUnicode(false);

            modelBuilder.Entity<jos_phocadownload>()
                .Property(e => e.metakey)
                .IsUnicode(false);

            modelBuilder.Entity<jos_phocadownload>()
                .Property(e => e.metadesc)
                .IsUnicode(false);

            modelBuilder.Entity<jos_phocadownload_categories>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<jos_phocadownload_categories>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<jos_phocadownload_categories>()
                .Property(e => e.alias)
                .IsUnicode(false);

            modelBuilder.Entity<jos_phocadownload_categories>()
                .Property(e => e.image)
                .IsUnicode(false);

            modelBuilder.Entity<jos_phocadownload_categories>()
                .Property(e => e.image_position)
                .IsUnicode(false);

            modelBuilder.Entity<jos_phocadownload_categories>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<jos_phocadownload_categories>()
                .Property(e => e.editor)
                .IsUnicode(false);

            modelBuilder.Entity<jos_phocadownload_categories>()
                .Property(e => e.uploaduserid)
                .IsUnicode(false);

            modelBuilder.Entity<jos_phocadownload_categories>()
                .Property(e => e.accessuserid)
                .IsUnicode(false);

            modelBuilder.Entity<jos_phocadownload_categories>()
                .Property(e => e.deleteuserid)
                .IsUnicode(false);

            modelBuilder.Entity<jos_phocadownload_categories>()
                .Property(e => e.name_params)
                .IsUnicode(false);

            modelBuilder.Entity<jos_phocadownload_categories>()
                .Property(e => e.metakey)
                .IsUnicode(false);

            modelBuilder.Entity<jos_phocadownload_categories>()
                .Property(e => e.metadesc)
                .IsUnicode(false);

            modelBuilder.Entity<jos_phocadownload_licenses>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<jos_phocadownload_licenses>()
                .Property(e => e.alias)
                .IsUnicode(false);

            modelBuilder.Entity<jos_phocadownload_licenses>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<jos_phocadownload_sections>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<jos_phocadownload_sections>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<jos_phocadownload_sections>()
                .Property(e => e.alias)
                .IsUnicode(false);

            modelBuilder.Entity<jos_phocadownload_sections>()
                .Property(e => e.image)
                .IsUnicode(false);

            modelBuilder.Entity<jos_phocadownload_sections>()
                .Property(e => e.scope)
                .IsUnicode(false);

            modelBuilder.Entity<jos_phocadownload_sections>()
                .Property(e => e.image_position)
                .IsUnicode(false);

            modelBuilder.Entity<jos_phocadownload_sections>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<jos_phocadownload_sections>()
                .Property(e => e.name_params)
                .IsUnicode(false);

            modelBuilder.Entity<jos_phocadownload_sections>()
                .Property(e => e.metakey)
                .IsUnicode(false);

            modelBuilder.Entity<jos_phocadownload_sections>()
                .Property(e => e.metadesc)
                .IsUnicode(false);

            modelBuilder.Entity<jos_phocadownload_settings>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<jos_phocadownload_settings>()
                .Property(e => e.value)
                .IsUnicode(false);

            modelBuilder.Entity<jos_phocadownload_settings>()
                .Property(e => e.values)
                .IsUnicode(false);

            modelBuilder.Entity<jos_phocadownload_settings>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<jos_plugins>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<jos_plugins>()
                .Property(e => e.element)
                .IsUnicode(false);

            modelBuilder.Entity<jos_plugins>()
                .Property(e => e.folder)
                .IsUnicode(false);

            modelBuilder.Entity<jos_plugins>()
                .Property(e => e.name_params)
                .IsUnicode(false);

            modelBuilder.Entity<jos_poll_data>()
                .Property(e => e.text)
                .IsUnicode(false);

            modelBuilder.Entity<jos_polls>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<jos_polls>()
                .Property(e => e.alias)
                .IsUnicode(false);

            modelBuilder.Entity<jos_rokdownloads>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<jos_rokdownloads>()
                .Property(e => e.displayname)
                .IsUnicode(false);

            modelBuilder.Entity<jos_rokdownloads>()
                .Property(e => e.path)
                .IsUnicode(false);

            modelBuilder.Entity<jos_rokdownloads>()
                .Property(e => e.introtext)
                .IsUnicode(false);

            modelBuilder.Entity<jos_rokdownloads>()
                .Property(e => e.fulltext)
                .IsUnicode(false);

            modelBuilder.Entity<jos_rokdownloads>()
                .Property(e => e.thumbnail)
                .IsUnicode(false);

            modelBuilder.Entity<jos_rokdownloads>()
                .Property(e => e.name_params)
                .IsUnicode(false);

            modelBuilder.Entity<jos_rokdownloads>()
                .Property(e => e.metadata)
                .IsUnicode(false);

            modelBuilder.Entity<jos_rokdownloads>()
                .Property(e => e.metadesc)
                .IsUnicode(false);

            modelBuilder.Entity<jos_rokdownloads>()
                .Property(e => e.metakey)
                .IsUnicode(false);

            modelBuilder.Entity<jos_sections>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<jos_sections>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<jos_sections>()
                .Property(e => e.alias)
                .IsUnicode(false);

            modelBuilder.Entity<jos_sections>()
                .Property(e => e.image)
                .IsUnicode(false);

            modelBuilder.Entity<jos_sections>()
                .Property(e => e.scope)
                .IsUnicode(false);

            modelBuilder.Entity<jos_sections>()
                .Property(e => e.image_position)
                .IsUnicode(false);

            modelBuilder.Entity<jos_sections>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<jos_sections>()
                .Property(e => e.name_params)
                .IsUnicode(false);

            modelBuilder.Entity<jos_sefexts>()
                .Property(e => e.file)
                .IsUnicode(false);

            modelBuilder.Entity<jos_sefexts>()
                .Property(e => e.name_params)
                .IsUnicode(false);

            modelBuilder.Entity<jos_sefexts>()
                .Property(e => e.filters)
                .IsUnicode(false);

            modelBuilder.Entity<jos_sefexts>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<jos_sefexttexts>()
                .Property(e => e.extension)
                .IsUnicode(false);

            modelBuilder.Entity<jos_sefexttexts>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<jos_sefexttexts>()
                .Property(e => e.value)
                .IsUnicode(false);

            modelBuilder.Entity<jos_sefmoved>()
                .Property(e => e.old)
                .IsUnicode(false);

            modelBuilder.Entity<jos_sefmoved>()
                .Property(e => e.name_new)
                .IsUnicode(false);

            modelBuilder.Entity<jos_sefurls>()
                .Property(e => e.sefurl)
                .IsUnicode(false);

            modelBuilder.Entity<jos_sefurls>()
                .Property(e => e.origurl)
                .IsUnicode(false);

            modelBuilder.Entity<jos_sefurls>()
                .Property(e => e.Itemid)
                .IsUnicode(false);

            modelBuilder.Entity<jos_sefurls>()
                .Property(e => e.metadesc)
                .IsUnicode(false);

            modelBuilder.Entity<jos_sefurls>()
                .Property(e => e.metakey)
                .IsUnicode(false);

            modelBuilder.Entity<jos_sefurls>()
                .Property(e => e.metatitle)
                .IsUnicode(false);

            modelBuilder.Entity<jos_sefurls>()
                .Property(e => e.metalang)
                .IsUnicode(false);

            modelBuilder.Entity<jos_sefurls>()
                .Property(e => e.metarobots)
                .IsUnicode(false);

            modelBuilder.Entity<jos_sefurls>()
                .Property(e => e.metagoogle)
                .IsUnicode(false);

            modelBuilder.Entity<jos_sefurls>()
                .Property(e => e.canonicallink)
                .IsUnicode(false);

            modelBuilder.Entity<jos_sefurls>()
                .Property(e => e.trace)
                .IsUnicode(false);

            modelBuilder.Entity<jos_sefurls>()
                .Property(e => e.sm_frequency)
                .IsUnicode(false);

            modelBuilder.Entity<jos_sefurls>()
                .Property(e => e.sm_priority)
                .IsUnicode(false);

            modelBuilder.Entity<jos_sefwords>()
                .Property(e => e.word)
                .IsUnicode(false);

            modelBuilder.Entity<jos_session>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<jos_session>()
                .Property(e => e.time)
                .IsUnicode(false);

            modelBuilder.Entity<jos_session>()
                .Property(e => e.session_id)
                .IsUnicode(false);

            modelBuilder.Entity<jos_session>()
                .Property(e => e.usertype)
                .IsUnicode(false);

            modelBuilder.Entity<jos_session>()
                .Property(e => e.data)
                .IsUnicode(false);

            modelBuilder.Entity<jos_tag_category_map>()
                .Property(e => e.scope)
                .IsUnicode(false);

            modelBuilder.Entity<jos_tag_hideshow>()
                .Property(e => e.scope)
                .IsUnicode(false);

            modelBuilder.Entity<jos_tag_hideshow>()
                .Property(e => e.spare)
                .IsUnicode(false);

            modelBuilder.Entity<jos_tag_layouts>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<jos_tag_layouts>()
                .Property(e => e.filename)
                .IsUnicode(false);

            modelBuilder.Entity<jos_tag_layouts>()
                .Property(e => e.appliesto)
                .IsUnicode(false);

            modelBuilder.Entity<jos_tag_layouts>()
                .Property(e => e.desc)
                .IsUnicode(false);

            modelBuilder.Entity<jos_tag_tags>()
                .Property(e => e.tagname)
                .IsUnicode(false);

            modelBuilder.Entity<jos_tag_tags>()
                .Property(e => e.component)
                .IsUnicode(false);

            modelBuilder.Entity<jos_tag_tags>()
                .Property(e => e.template)
                .IsUnicode(false);

            modelBuilder.Entity<jos_tag_tags>()
                .Property(e => e.output)
                .IsUnicode(false);

            modelBuilder.Entity<jos_tag_tags>()
                .Property(e => e.sef)
                .IsUnicode(false);

            modelBuilder.Entity<jos_tag_tags>()
                .Property(e => e.tagtext)
                .IsUnicode(false);

            modelBuilder.Entity<jos_tag_tags>()
                .Property(e => e.desc)
                .IsUnicode(false);

            modelBuilder.Entity<jos_tag_tags>()
                .Property(e => e.meta_title)
                .IsUnicode(false);

            modelBuilder.Entity<jos_tag_tags>()
                .Property(e => e.meta_desc)
                .IsUnicode(false);

            modelBuilder.Entity<jos_tag_tags>()
                .Property(e => e.meta_keywords)
                .IsUnicode(false);

            modelBuilder.Entity<jos_tag_tags>()
                .Property(e => e.layout_dir)
                .IsUnicode(false);

            modelBuilder.Entity<jos_tag_tags>()
                .Property(e => e.layout_orderby)
                .IsUnicode(false);

            modelBuilder.Entity<jos_templates_menu>()
                .Property(e => e.template)
                .IsUnicode(false);

            modelBuilder.Entity<jos_users>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<jos_users>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<jos_users>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<jos_users>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<jos_users>()
                .Property(e => e.usertype)
                .IsUnicode(false);

            modelBuilder.Entity<jos_users>()
                .Property(e => e.activation)
                .IsUnicode(false);

            modelBuilder.Entity<jos_users>()
                .Property(e => e.name_params)
                .IsUnicode(false);

            modelBuilder.Entity<jos_weblinks>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<jos_weblinks>()
                .Property(e => e.alias)
                .IsUnicode(false);

            modelBuilder.Entity<jos_weblinks>()
                .Property(e => e.url)
                .IsUnicode(false);

            modelBuilder.Entity<jos_weblinks>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<jos_weblinks>()
                .Property(e => e.name_params)
                .IsUnicode(false);

            modelBuilder.Entity<jos_wf_profiles>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<jos_wf_profiles>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<jos_wf_profiles>()
                .Property(e => e.users)
                .IsUnicode(false);

            modelBuilder.Entity<jos_wf_profiles>()
                .Property(e => e.types)
                .IsUnicode(false);

            modelBuilder.Entity<jos_wf_profiles>()
                .Property(e => e.components)
                .IsUnicode(false);

            modelBuilder.Entity<jos_wf_profiles>()
                .Property(e => e.device)
                .IsUnicode(false);

            modelBuilder.Entity<jos_wf_profiles>()
                .Property(e => e.rows)
                .IsUnicode(false);

            modelBuilder.Entity<jos_wf_profiles>()
                .Property(e => e.plugins)
                .IsUnicode(false);

            modelBuilder.Entity<jos_wf_profiles>()
                .Property(e => e.name_params)
                .IsUnicode(false);

            modelBuilder.Entity<jos_xmap>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<jos_xmap>()
                .Property(e => e.value)
                .IsUnicode(false);

            modelBuilder.Entity<jos_xmap_ext>()
                .Property(e => e.extension)
                .IsUnicode(false);

            modelBuilder.Entity<jos_xmap_ext>()
                .Property(e => e.name_params)
                .IsUnicode(false);

            modelBuilder.Entity<jos_xmap_items>()
                .Property(e => e.uid)
                .IsUnicode(false);

            modelBuilder.Entity<jos_xmap_items>()
                .Property(e => e.view)
                .IsUnicode(false);

            modelBuilder.Entity<jos_xmap_items>()
                .Property(e => e.properties)
                .IsUnicode(false);

            modelBuilder.Entity<jos_xmap_sitemap>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<jos_xmap_sitemap>()
                .Property(e => e.ext_image)
                .IsUnicode(false);

            modelBuilder.Entity<jos_xmap_sitemap>()
                .Property(e => e.menus)
                .IsUnicode(false);

            modelBuilder.Entity<jos_xmap_sitemap>()
                .Property(e => e.exclmenus)
                .IsUnicode(false);

            modelBuilder.Entity<jos_xmap_sitemap>()
                .Property(e => e.classname)
                .IsUnicode(false);

            modelBuilder.Entity<jos_xmap_sitemap>()
                .Property(e => e.excluded_items)
                .IsUnicode(false);

            modelBuilder.Entity<jos_yvcomment>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<jos_yvcomment>()
                .Property(e => e.alias)
                .IsUnicode(false);

            modelBuilder.Entity<jos_yvcomment>()
                .Property(e => e.title_alias)
                .IsUnicode(false);

            modelBuilder.Entity<jos_yvcomment>()
                .Property(e => e.introtext)
                .IsUnicode(false);

            modelBuilder.Entity<jos_yvcomment>()
                .Property(e => e.fulltext)
                .IsUnicode(false);

            modelBuilder.Entity<jos_yvcomment>()
                .Property(e => e.created_by_alias)
                .IsUnicode(false);

            modelBuilder.Entity<jos_yvcomment>()
                .Property(e => e.images)
                .IsUnicode(false);

            modelBuilder.Entity<jos_yvcomment>()
                .Property(e => e.urls)
                .IsUnicode(false);

            modelBuilder.Entity<jos_yvcomment>()
                .Property(e => e.attribs)
                .IsUnicode(false);

            modelBuilder.Entity<jos_yvcomment>()
                .Property(e => e.metakey)
                .IsUnicode(false);

            modelBuilder.Entity<jos_yvcomment>()
                .Property(e => e.metadesc)
                .IsUnicode(false);

            modelBuilder.Entity<jos_yvcomment>()
                .Property(e => e.metadata)
                .IsUnicode(false);

            modelBuilder.Entity<jos_bfdbversions>()
                .Property(e => e.tablename)
                .IsUnicode(false);

            modelBuilder.Entity<jos_bfdbversions>()
                .Property(e => e.version)
                .IsUnicode(false);

            modelBuilder.Entity<jos_bfdbversions>()
                .Property(e => e.component)
                .IsUnicode(false);

            modelBuilder.Entity<jos_core_acl_groups_aro_map>()
                .Property(e => e.section_value)
                .IsUnicode(false);

            modelBuilder.Entity<jos_core_log_items>()
                .Property(e => e.item_table)
                .IsUnicode(false);

            modelBuilder.Entity<jos_core_log_searches>()
                .Property(e => e.search_term)
                .IsUnicode(false);

            modelBuilder.Entity<jos_messages_cfg>()
                .Property(e => e.cfg_name)
                .IsUnicode(false);

            modelBuilder.Entity<jos_messages_cfg>()
                .Property(e => e.cfg_value)
                .IsUnicode(false);

            modelBuilder.Entity<jos_rokversions>()
                .Property(e => e.product)
                .IsUnicode(false);

            modelBuilder.Entity<jos_rokversions>()
                .Property(e => e.version)
                .IsUnicode(false);

            modelBuilder.Entity<jos_stats_agents>()
                .Property(e => e.agent)
                .IsUnicode(false);
        }
    }
}
