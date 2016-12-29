<%@ Page Language="C#" AutoEventWireup="true" Inherits="_Default" Codebehind="Default.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Rmes系统登录</title>
        <script  type="text/javascript" language="javascript"   src="./Rmes/Login/Menu.js"></script> 
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label1" runat="server" Text="tiaozhuan" Width="86px"></asp:Label><br />
        <table style="width: 900px" border ="2">
            <tr>
                <td colspan="3">
                    kankan</td>
            </tr>
            <tr>
                <td colspan="3" style="height: 122px">   
                <script type="text/javascript" language="javascript">



//处理菜单生成
var arrayMenu1=arrayMenu;
var arrayMenu2=arrayMenu;

for(var i=0;i<arrayMenu.length;i++){
    
 
    var menuCode = arrayMenu[i][0];
    var menuName1 = arrayMenu[i][1];
    var menuCodeFather = arrayMenu[i][2];
    var menuLevel = arrayMenu[i][3];
    var leafFlag = arrayMenu[i][4];
    var programCode = arrayMenu[i][5];
    
 
 
    if(menuLevel==0){
 
    //显示一级菜单
        with(milonic=new menuname(String(menuName1))){
            
            alwaysvisible=1;
            //left=10;
            orientation="horizontal";
            style=menuStyle;
            //top=10;
            position="relative";
            aI("align=left;text="+menuName1+";");
            
            //继续从数组里面循环  11
            for(var j=0;j<arrayMenu1.length;j++){
             
             
                if((parseInt(menuLevel)+1==parseInt(arrayMenu1[j][3]))&&(String(menuCode)==String(arrayMenu1[j][2]))){
 
                    aI("align=left;showmenu="+arrayMenu1[j][1]+";text="+arrayMenu1[j][1]+";");
                }
            }

        }
    }else {
    //子菜单
        with(milonic=new menuname(String(menuName1))){
            style=submenuStyle;
            if(menuLevel==1){
                top="offset=-7";
            }else{
                top="offset=-4";
                left="offset=-3";
            }
                
            
            //如果不是叶子节点，创建子菜单
            if(String(leafFlag)=="N"){
 
            for(var k=0;k<arrayMenu2.length;k++){

                   if((parseInt(menuLevel)+1==parseInt(arrayMenu2[k][3]))&&(String(menuCode)==String(arrayMenu1[k][2]))){
                    if((arrayMenu2[k][4]=="N"))
                        aI("align=left;showmenu="+arrayMenu2[k][1]+";text="+arrayMenu2[k][1]+";");
                    else
                        aI("text="+arrayMenu2[k][1]+";url="+arrayMenu2[k][5]+";");
 
                }
            }

            }
        }
 
   }
}
drawMenus();
</script>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    bbbbb</td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

<script type="text/javascript" language="javascript">

/*

//处理菜单生成
var arrayMenu1=arrayMenu;
var arrayMenu2=arrayMenu;

for(var i=0;i<arrayMenu.length;i++){
    
 
    var menuCode = arrayMenu[i][0];
    var menuName1 = arrayMenu[i][1];
    var menuCodeFather = arrayMenu[i][2];
    var menuLevel = arrayMenu[i][3];
    var leafFlag = arrayMenu[i][4];
    var programCode = arrayMenu[i][5];
    
 
 
    if(menuLevel==0){
 
    //显示一级菜单
        with(milonic=new menuname(String(menuName1))){
            
            alwaysvisible=1;
            left=10;
            orientation="horizontal";
            style=menuStyle;
            top=10;
            aI("align=left;text="+menuName1+";");
            
            //继续从数组里面循环
            for(var j=0;j<arrayMenu1.length;j++){
             
             
                if((parseInt(menuLevel)+1==parseInt(arrayMenu1[j][3]))&&(String(menuCode)==String(arrayMenu1[j][2]))){
 
                    aI("align=left;showmenu="+arrayMenu1[j][1]+";text="+arrayMenu1[j][1]+";");
                }
            }

        }
    }else {
    //子菜单
        with(milonic=new menuname(String(menuName1))){
            style=submenuStyle;
            if(menuLevel==1){
                top="offset=-7";
            }else{
                top="offset=-4";
                left="offset=-3";
            }
                
            
            //如果不是叶子节点，创建子菜单
            if(String(leafFlag)=="N"){
 
            for(var k=0;k<arrayMenu2.length;k++){

                   if((parseInt(menuLevel)+1==parseInt(arrayMenu2[k][3]))&&(String(menuCode)==String(arrayMenu1[k][2]))){
                    if((arrayMenu2[k][4]=="N"))
                        aI("align=left;showmenu="+arrayMenu2[k][1]+";text="+arrayMenu2[k][1]+";");
                    else
                        aI("text="+arrayMenu2[k][1]+";url="+arrayMenu2[k][5]+";");
 
                }
            }

            }
        }
 
   }
}
drawMenus();
*/
</script>