﻿<%@ Page Title="" Language="C#" MasterPageFile="~/GoC.WebTemplate/GoCWebTemplate.BilingualErrorPage.Master" AutoEventWireup="true" CodeBehind="TestErrorPage.aspx.cs" Inherits="GoC.WebTemplate.WebForms.TestErrorPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="wb-inv" id="wb-cont">We couldn't find that Web page <span lang="fr">Nous ne pouvons trouver cette page Web</span></h1>
    <div class="row mrgn-tp-lg mrgn-bttm-lg">
        <div class="col-md-12">
            <div class="row">
                <section class="col-md-6">
                    <div class="row">
                        <div class="col-xs-3 col-sm-2 col-md-2 text-center mrgn-tp-md"> <span class="glyphicon glyphicon-warning-sign glyphicon-error"></span> </div>
                        <div class="col-xs-9 col-sm-10 col-md-10">
                            <h2 class="mrgn-tp-md">We couldn't find that Web page</h2>
                            <p class="pagetag"><b>Error 404</b></p>
                        </div>
                    </div>
                    <p>We're sorry you ended up here. Sometimes a page gets moved or deleted, but hopefully we can help you find what you're looking for. What next?</p>
                    <ul>
                        <li>Return to the <a href="/en.html">home page</a>;</li>
                        <li>Consult the <a href="/en/sitemap.html">site map</a>;</li>
                        <li><a href="/en/contact.html">Contact us</a> and we'll help you out.</li>
                    </ul>
                    <section class="col-md-12">
                        <h3 class="wb-inv">Search</h3>
                        <form action="https://recherche-search.gc.ca/rGs/s_r?#wb-land" method="get" name="cse-search-box" role="search" class="form-inline">
                            <div class="form-group">
                                <label for="wb-srch-q-en" class="wb-inv">Search website</label>
                                <input name="cdn" value="canada" type="hidden" />
                                <input name="st" value="s" type="hidden" />
                                <input name="num" value="10" type="hidden" />
                                <input name="langs" value="eng" type="hidden" />
                                <input name="st1rt" value="0" type="hidden" />
                                <input name="s5bm3ts21rch" value="x" type="hidden" />
                                <input name="_charset_" value="UTF-8" type="hidden" />
                                <input id="wb-srch-q-en" list="wb-srch-q-ac-en" class="wb-srch-q form-control" name="q" value="" size="27" maxlength="150" placeholder="Search Canada.ca" type="search" />
                                <datalist id="wb-srch-q-ac-en">
                                    <!--[if lte IE 9]><select><![endif]-->
                                    <!--[if lte IE 9]></select><![endif]-->
                                </datalist>
                            </div>
                            <div class="form-group submit">
                                <button type="submit" id="wb-srch-sub-en" class="btn btn-primary btn-small" name="wb-srch-sub-en"><span class="glyphicon-search glyphicon"></span><span class="wb-inv">Search</span></button>
                            </div>
                        </form>
                    </section>
                </section>
                <section class="col-md-6" lang="fr">
                    <div class="row">
                        <div class="col-xs-3 col-sm-2 col-md-2 text-center mrgn-tp-md"> <span class="glyphicon glyphicon-warning-sign glyphicon-error"></span> </div>
                        <div class="col-xs-9 col-sm-10 col-md-10">
                            <h2 class="mrgn-tp-md">Nous ne pouvons trouver cette page Web</h2>
                            <p class="pagetag"><b>Erreur 404</b></p>
                        </div>
                    </div>
                    <p>Nous sommes dÃ©solÃ©s que vous ayez abouti ici. Il arrive parfois qu'une page ait Ã©tÃ© dÃ©placÃ©e ou supprimÃ©e. Heureusement, nous pouvons vous aider Ã  trouver ce que vous cherchez. Que faire?</p>
                    <ul>
                        <li>Retournez Ã  la <a href="/fr.html">page d'accueil</a>;</li>
                        <li>Consultez le <a href="/fr/plan.html">plan du site</a>;</li>
                        <li><a href="/fr/contact/index.html">Communiquez avec nous</a> pour obtenir de l'aide.</li>
                    </ul>
                    <section class="col-md-12">
                        <h3 class="wb-inv">Recherche</h3>
                        <form action="https://recherche-search.gc.ca/rGs/s_r?#wb-land" method="get" name="cse-search-box" role="search" class="form-inline">
                            <div class="form-group">
                                <label for="wb-srch-q-fr" class="wb-inv">Recherchez le site Web</label>
                                <input name="cdn" value="canada" type="hidden" />
                                <input name="st" value="s" type="hidden" />
                                <input name="num" value="10" type="hidden" />
                                <input name="langs" value="fra" type="hidden" />
                                <input name="st1rt" value="0" type="hidden" />
                                <input name="s5bm3ts21rch" value="x" type="hidden" />
                                <input name="_charset_" value="UTF-8" type="hidden" />
                                <input id="wb-srch-q-fr" list="wb-srch-q-ac-fr" class="wb-srch-q form-control" name="q" value="" size="27" maxlength="150" placeholder="Rechercher dans Canada.ca" type="search" />
                                <datalist id="wb-srch-q-ac-fr">
                                    <!--[if lte IE 9]><select><![endif]-->
                                    <!--[if lte IE 9]></select><![endif]-->
                                </datalist>
                            </div>
                            <div class="form-group submit">
                                <button type="submit" id="wb-srch-sub-fr" class="btn btn-primary btn-small" name="wb-srch-sub-fr"><span class="glyphicon-search glyphicon"></span><span class="wb-inv">Recherche</span></button>
                            </div>
                        </form>
                    </section>
                </section>
            </div>
        </div>
    </div>
</asp:Content>
