ALTER TABLE [dbo].[CurrentFiles] ADD [FileId1] [int] NOT NULL DEFAULT 0
ALTER TABLE [dbo].[CurrentFiles] ADD [FileId2] [int] NOT NULL DEFAULT 0
ALTER TABLE [dbo].[CurrentFiles] ADD [FileId3] [int] NOT NULL DEFAULT 0
INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'201712040628193_FileCurrentFileRel', N'Dunfoss.Data.EfDbContext',  0x1F8B0800000000000400ED5BDB6EE336107D2FD07F10F4D402D928B67369037B175B3B5904CD0D71B2EFB4443B42254A15A9D4FEB63EF493FA0B1D4AA2AE542C5A92375B040B2C6C9A7366383CC3CB0CF3EFDFFF8C3FAD5D477BC101B53D32D1078747BA8689E95936594DF4902D3FFCA27FFAF8E30FE30BCB5D6B5F45BF11EF0792844EF467C6FC73C3A0E63376113D746D33F0A8B76487A6E71AC8F28CE1D1D1AFC660606080D0014BD3C60F2161B68BA32FF075EA1113FB2C44CE8D67618726EDF0CB3C42D56E918BA98F4C3CD16721597A941ECE1043BAF6D9B111D830C7CE52D710211E430C2C3C7FA278CE028FACE63E3420E771E363E8B7440EC589E5E759F7A683381AF2411899A0803243CA3C571170304ABC6294C577F2AD9E7A0DFC7601FE651B3EEAC877137D1A060126ECD27660FC657DE75327E07D33EFC6F37098933AD092DF0E521A005BF8BF036D1A3A2C0CF084E09005C839D0EEC385639BBFE3CDA3F7072613123A4EDE3C30107E2B3440D37DE0F938609B07BC4C8CBEB274CD28CA1965C1542C27138FE58AB0D150D76E41395A38389DFDDCB8E7CC0BF0174C708018B6EE116338201C0347FEAB682FE90281E78150077C83A0D1B51BB4BEC664C59E273A7CD4B54B7B8D2DD19298F0446C8831106241889B6819EE45CBA8772D9C4957D660DB143501197601325201191B5958BD1A6C8A51F61E5EF594EC9D91FCFFDE9570F85E98768DB94F15B8160BBCB3ADAA0B0E14E91CF1CF8F362786E2AAE27A24E3EC6E0BD3CC7EB1697408E89B94DCA8FEB7AFE9330A58FF6AA2D1F4BF4D466AFADF2723A7ED6934C71DAB69BC783D60DF0B98C2E2150BBC2F5E3D2D5EB68B567B58102235FD733B52D37FA4466ABA0EA11A3527FB5173BA1F3567DF6ADD79A24A4726DEFD7DCDF94627E77B44E95F5E60ED952B5338C5211BFC921871B19C2D781B5ECB762BE047421D9AA82A8E28C69C63564DC5505DCBEC88D35887854C8DF13A541D4623E1F82A201317B78A2D00F1762C03103BFB16001E5932F1384065F395CE4C962234E21CA1C8251A35C9C4F10DF27DA04E2EB998B468F338B338FD30574FBCB93186615249FE2DB536D50411072B5FE957500D965EDA01653CABB9409CBC53CBAD74CBF3B0C6B34293846AE5152773BB10E29F93F3442ECB5A60640924F3E2250CCC855ED118716A4F31F358918E52BCC84181641D9B7A4EE892BAB5F035E9242F9707489AD43086558CA12AC6A88A3152C14833667994B451156728C3511A539A37ABE248C735364A0429D3D0A8F0B0B4E795B9DD88F92D29BF23D7F74DF22AB75410E20D3C8F10B7344788F35A7984B8E5CDD040EC713B1321D90AD5A95027D80F19E2FB5F5E3E6E698E9064AEF2104993821569EAAA6049DAAA40AC243755A056D2D61C45A49EF228A24DD196A1C416A57553248E2A284ABB81C80B5546A46ECBB1C496E33714BAE274B973E8268750F5D0AD137CABA12BF236790CD1A6883294A028514B645D2A284A341749950A8A94A0AFA29C48504E94514E2528A7CA28671294B3371472F17D6CE7808BAE6DEAE12617EB27D8DA1F79B28444F1E8255AF73C99958B71B94BAA3DBD20972EC2E3E452BAFDE94DE5961A77D13570D18B6DF11BEA7C431976633ACCFF74A68E0DE3CD3ADC20622F316571624C1F1E0D86A5373C6FE73D8D41A9E5347F54B3F7049FCD1DBB3585A7580228BC68212F2830E170F1938BD63FE7A1945FADB4461A7582547A7D1279B0DDDB93DD2146CD21D4DE9DFC7F88D8C99CE7B3D4AD80F26F383A9C33D93DF1BB9DB57C19D282CFACF51B8A5D02ACFC82A2DDB4175E49B4822ABE84686F5537CB6BF14543FB017668D5F10E50AAAF0FDE03AFA6FEDF6AFA8A35FEF650DDF0B358AB6F0F75D21DD4697750677D464DF5B2F6DDC64C672783728DBA33BF97EBD0D53261CDB55E723FD952688EAF72B0782C3C18466C6EB150DDA0145D5B8996A1378315A5E8DA1AB50C3AAD6C372A60BF52BF9681A755EF06C5EDDADAB60C38A986EF58F6AEDEE2C746FECF6CC6334CED5506C1FFE88660935F8F3350D1E78A2C3D41721851DE22D1A51403379821D878D0E780D94B6432F8D9C494466F35BE2227842E17EE025B57E42E647EC83E538ADD8553786432365ED71FD5F68B368FEF7CFE8D76310430D3E67BE71DF92DB41D2BB5FB5272BAAF81E0019AAC5060D59CF1956AB549916E3DD2102871DF0CFB98F0F5ED11BBBE0360F48ECCD10BDEC536A0D7355E2173239231F520DB27A2E8F6F1CC46AB00B934C1C8E4E12B70D872D71FFF0339D2448C6D360000 , N'6.2.0-61023')

