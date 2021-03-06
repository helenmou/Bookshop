﻿<%@ Page Title="" Language="C#" MasterPageFile="~/shared/Site.Master" AutoEventWireup="true" CodeBehind="cart.aspx.cs" Inherits="WebApp.cart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    
        <!-- 购物车开始 -->
        <div class="row">
            <div class="col-lg-12">
                <div class="cart-title f16 tit-family pl10 mt10">我的购物车</div>
                <div class="cart-content">
                    <table width="100%" border="0">
                        <tbody>
                            <tr>
                                <td width="5%" class="tr-title">
                                    <form>
                                        <div class="checkbox">
                                            <label>
                                                <input type="checkbox" />全选</label>
                                        </div>
                                    </form>
                                </td>
                                <td width="45%" class="tr-title">商品名称</td>
                                <td width="7%" class="tr-title">积分</td>
                                <td width="12%" class="tr-title">金额</td>
                                <td width="11%" class="tr-title">优惠</td>
                                <td width="12%" class="tr-title">数量</td>
                                <td width="8%" class="tr-title">操作</td>
                            </tr>
                             <%
                                 double totalPrice = 0;
                                 int prdAmount=0 ;
                                 foreach (BookShop.Model.CartInfo item in CartList)
                                 {
                                     prdAmount += item.Count;
                                     totalPrice += item.Count * item.Price;
                             %>
                            <tr>
                                <td width="5%" class="tr-list">
                                    <form>
                                        <div class="checkbox">
                                            <label>
                                                <input type="checkbox" /></label>
                                        </div>
                                    </form>
                                </td>
                                <td width="45%" class="tr-list">
                                    <a href=""><img class="pull-left" alt="" src="<%= WebApp.UrlHelper.PicUrl(item.ISBN) %>" /></a>
                                    <div class="summary blue-font"><a href="<%= WebApp.UrlHelper.Detail(item.ProductId) %>"><%= item.Title%></a></div>
                                </td>
                                <td width="7%" class="tr-list"><b>0</b></td>
                                <td width="12%" class="tr-list"><b class="orange-font">￥<%= item.Price %> </b></td>
                                <td width="11%" class="tr-list">减￥0.00</td>
                                <td width="12%" class="tr-list">
                                    <span class="ui-spinner">
                                        <input type="text" value="<%= item.Count %>" aria-valuenow="0" autocomplete="off">
                                        <a class="ui-spinner-button ui-spinner-up" tabindex="-1"><span class="ui-icon">▲</span></a>
                                        <a class="ui-spinner-button ui-spinner-down" tabindex="-1"><span class="ui-icon">▼</span></a>
                                    </span>
                                </td>
                                <td width="8%" class="tr-list bule-font"><a href="">删除</a></td>
                            </tr>
                           <%
                              } 
                        %>
                        </tbody>
                    </table>
                </div>
            </div>
            <div class="col-lg-12 main-show mb10">
                <div class="p10">
                    <div class="show-left pull-left">
                        <a href=""><i class="icon-main icon-fork"></i>删除选中的商品</a>
                    </div>
                    <div class="show-right pull-right tr">
                        <div class=""><b class="orange-font"><%=prdAmount %></b> 件商品 总计：￥<%=totalPrice %></div>
                        <div class="">返现：-￥0.00</div>
                    </div>
                    <div class="clearfix"></div>
                </div>
                <div class="total tr"><b>总计（不含运费）：</b> <i class="orange-font f20 tit-family pr10">￥<%=totalPrice %></i></div>
            </div>
            <div class="pull-right">
                <button type="button" class="btn btn-addcart btn-lg mr20">继续购物</button>
                <button type="button" class="btn btn-danger btn-lg mr20">去结算</button>
            </div>
        </div>
        <!-- 购物车结束 -->
        <!-- 购物车为空开始 -->
        <div class="row">
            <div class="col-lg-12">
                <div class="cart-title f16 tit-family pl10 mt10">我的购物车</div>
                <div class="cart-content p30">
                    <div class="pull-left empty-img"><img alt="" src="assets/img/empty-cart.jpg" /></div>
                    <div class="pull-right empty-text tit-family">
                        您的购物车中还没有商品，您现在可以<br />
                        <b class="f14">马上去 <i><a href="">挑选商品</a></i>， 或者查查 <i><a href="">我的订单</a></i></b>
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
        <!-- 购物车为空结束 -->
        
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="foot" runat="server">
</asp:Content>
