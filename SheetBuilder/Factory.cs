using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SheetBuilder.Models;
using System.IO;

namespace SheetBuilder
{
    public class Factory
    {

        public static void GenerateFile(string content, string path, string fileName)
        {
            using (StreamWriter w = new StreamWriter($"{path}/{fileName}.html"))
            {
                w.Write(content);
            }

            using (StreamWriter w = new StreamWriter($"{path}/styles.css"))
            {
                w.Write(cssContent());
            }
        }

        public static string Build(List<Paper> papers)
        {
            StringBuilder template = new StringBuilder();
            template.Append("<!DOCTYPE html>");
            template.Append("<html>");
                template.Append("<head>");
            template.Append("<meta charset='utf-8' />");
                    template.Append("<title></title>");
                    template.Append("<link rel='stylesheet' type='text/css' href='styles.css'>");
                template.Append("</head>");
                template.Append("<body>");
                    foreach (Paper paper in papers)
                    {
                        template.Append(GetPages(paper));
                    }
                template.Append("</body>");
            template.Append("</html>");
            return template.ToString();
        }

        private static string GetPages(Paper paper)
        {
            StringBuilder content = new StringBuilder();

            content.Append("<page size='Letter'>");
           
                for (var x=0; x < paper.Asignaciones.Count; x++)
                {
                    if (x < 4)
                    {
                        content.Append(GetSheets(paper.Asignaciones[x], x+1, paper.Fecha));
                    }
                }

            content.Append("</page>");

            return content.ToString();
        }

        private static string GetSheets(Asignacion asignacion, int counter, string fecha)
        {
            StringBuilder content = new StringBuilder();

            content.Append($"<div class='sheet p{counter}'>");
                content.Append("<div class='titulo'>ASIGNACIÓN PARA LA REUNIÓN VIDA Y MINISTERIO CRISTIANOS</div>");

                content.Append("<ul class='datos'>");
                    content.Append($"<li>Nombre: <span id='nombre' class='datos-text'>{asignacion.Asignado}</span></li>");
                    content.Append($"<li>Ayudante:<span id='ayudante' class='datos-text'>{asignacion.Ayudante}</span></li>");
                    content.Append($"<li>Fecha:<span id='fecha' class='datos-text'>{fecha}</span></li>");
                    content.Append($"<li>Aspecto de la oratoria: <span id='aspecto' class='datos-text'>{asignacion.Leccion}</span></li>");
                content.Append("</ul>");

                content.Append("<ul class='subtitulo'><li>Tipo de intervención:</li></ul>");            

                content.Append("<ul class='options-box col1'>");
                    content.Append($"<li><input type='checkbox' {(asignacion.Lectura ? "checked" : string.Empty)} />Lectura de la Biblia</li>");
                    content.Append($"<li><input type='checkbox' {(asignacion.PrimeraConversacion ? "checked" : string.Empty)} />Primera conversación</li>");
                    content.Append($"<li><input type='checkbox' {(asignacion.PrimeraRevisita ? "checked" : string.Empty)} />Primera revisita</li>");
                    content.Append($"<li><input type='checkbox' {(asignacion.SegundaRevisita ? "checked" : string.Empty)} />Segunda revisita</li>");
                content.Append("</ul>");

                content.Append("<ul class='options-box col2'>");
                    content.Append($"<li><input type='checkbox' {(asignacion.TerceraRevisita ? "checked" : string.Empty)} />Tercera revisita</li>");
                    content.Append($"<li><input type='checkbox' {(asignacion.CursoBiblico ? "checked" : string.Empty)} />Curso bíblico </li>");
                    content.Append($"<li><input type='checkbox' {(asignacion.Discurso ? "checked" : string.Empty)} />Discurso</li>");
                    content.Append($"<li><input type='checkbox' {(asignacion.Otro ? "checked" : string.Empty)} />Otro:________</li> ");
                content.Append("</ul>");

                content.Append("<ul class='subtitulo2'><li>Se presentará en:</li></ul>");

                content.Append("<ul class='options-box col3'>");
                    content.Append($"<li><input type='checkbox' {(asignacion.SalaPrincipal ? "checked" : string.Empty)} />Sala principal</li>");
                    content.Append($"<li><input type='checkbox' {(asignacion.SalaAux1? "checked" : string.Empty)} />Sala auxiliar núm. 1</li>");
                    content.Append($"<li><input type='checkbox' {(asignacion.SalaAux2 ? "checked" : string.Empty)} />Sala auxiliar núm. 2</li>");
                content.Append("</ul>");

                content.Append("<div class='nota'>");
                    content.Append("<p>");
                        content.Append("<span class='n'>Nota al estudiante:</span> En la <span class='k'>Guía de actividades</span>");
                        content.Append("encontrará la información que necesita para su intervención.");
                        content.Append("Prepare con la ayuda del libro Benefíciese el aspecto de la oratoria que se le indica");
                        content.Append("en esta hoja.<span class='n'> No olvide llevar su libro a la reunión Vida y Ministerio</span>");
                    content.Append("</p>");
                content.Append("</div>");

                content.Append("<div class='footer'>S-89-S<span class='ref'>10/17</span></div>");

            content.Append("</div>");

            return content.ToString();
        }

        public static string cssContent()
        {
            return ".p2,.p4{float:right;position:relative}.options-box,.p1,.p2,.p3,.p4,.titulo{position:relative}.nota .n,.titulo{font-weight:700}html{font-family:Arial,Helvetica,sans-serif;height:21.6cm}body{background:#ccc}.sheet,page{background:#fff}page{display:block;margin:0 auto .5cm;box-shadow:0 0 .5cm rgba(0,0,0,.5)}page[size=Letter]{width:21.6cm;height:27.9cm}@media print{body,page{margin:0;box-shadow:0}}.sheet{border-style:solid;border-color:#000;width:9cm;height:12cm}.p1{top:1.5cm;left:1.1cm}.p2{top:-10.6cm;left:-1cm}.p3{top:2.3cm;left:1.1cm}.p4{top:-10cm;left:-1cm}.titulo{text-align:center;top:.3cm;width:7cm;left:1cm}.options-box{font-size:15px;list-style:none;top:-.2cm}.footer,.nota{font-size:12.4px;position:relative}.col1{left:-.9cm}.col2{top:-2.6cm;left:4.1cm;width:3.8cm}.col3{top:-3.1cm;left:-.9cm}.nota{top:-3.2cm;margin:.1cm}.nota .k{font-style:italic}.footer{top:-3cm;left:.1cm}.ref{position:relative;left:.5cm}.datos,.subtitulo{position:relative;left:-.9cm;top:.1cm}.datos li,.subtitulo li,.subtitulo2 li{list-style:none;font-size:16px;font-weight:700}.subtitulo2{position:relative;left:-.9cm;top:-2.8cm}.datos-text{font-size:11px;border-bottom-style:solid;border-bottom-color:#000;border-bottom-width:1px}#nombre{margin-left:.45cm}#ayudante{margin-left:.25cm}#fecha{margin-left:1cm}#aspecto{margin-left:1.3cm;font-size:14px!important}.grid2x2{min-height:100%;display:flex;flex-wrap:wrap;flex-direction:row}.grid2x2>div{display:flex;flex-basis:calc(50% - 40px);justify-content:center;flex-direction:column}.grid2x2>div>div{display:flex;justify-content:center;flex-direction:row}";
        }
    }
}
