/*******************************************************
* Copyright (C) 2016-2020 Manipal Technologies Pvt. Ltd.
* 
 * This file is part of the project OSR and has been exclusively created for internal use of
* Manipal Technologies Pvt. Ltd. or licensed use of clients of Manipal Technologies Pvt. Ltd.
* Under no circumstances, can this file/project could be used individually or as part of
* OSR application can be copied and/or distributed without the express
* permission of Manipal Technologies Pvt. Ltd.
*******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OMRManagement.Entities
{

    public enum RunType
    {
        Normal,
        Recovery,
        ForceOverwrite,
        ErrorOverwrite,
        UnMatched
    }

    public enum Status
    {
        SLEEP,
        STARTED,
        RUNNING,
        INTERRUPTED,
        WAITING,
        TERMINATED,
        FINISHED
    }

    public enum FieldType
    {
        HOR,
        VER
    }

    public enum PageType
    {
        [XmlEnum(Name = "0")]
        FRONT ,
        [XmlEnum(Name = "1")]
        BACK
    }


}
