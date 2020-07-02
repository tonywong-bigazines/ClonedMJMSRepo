<template>
  <div class="dashboard">
    <el-card class="box-card">
      <div slot="header" class="clearfix">
        <span>常用功能</span>
      </div>
      <div class="card-body">
        <el-button type="primary" round @click="showReadCardModal">結算</el-button>
      </div>
    </el-card>

    <el-dialog title="結算" :visible.sync="cardReader.visible" width="30%" :close-on-click-modal="false">
      <div class="input-card-msg" v-if="!cardReader.isReady">初始化中，請稍等...</div>
      <div class="input-card-msg" v-else>請拍卡...</div>
      <div class="input-card-wrapper">
        <el-input id="input-card" v-model="cardReader.cardId" @keyup.enter.native="gotoLiquidation" placeholder="或輸入Card number"></el-input>
      </div>
      <span slot="footer" class="dialog-footer">
        <el-button @click="hideReadCardModal">取消</el-button>
        <el-button type="primary" @click="gotoLiquidation">確定</el-button>
      </span>
    </el-dialog>
  </div>
</template>

<script>
export default {
  name: "dashboard",
  data(){
    return {
      dialogVisible:false,
      cardReader:{
        visible:false,
        isReady:false,
        cardId:null
      }
    };
  },
  methods:{
    showReadCardModal(){
      this.addListener(document.body,"click",this.focusInputCard);
      this.cardReader.visible=true;
      this.cardReader.isReady = false;
      this.cardReader.cardId = "";
      setTimeout(()=>{        
        document.getElementById("input-card").focus();
        this.cardReader.isReady = true;
      },1000);      
    },
    hideReadCardModal(){
      this.removeListener(document.body,"click",this.focusInputCard);
      this.cardReader.visible=false;
    },
    gotoLiquidation(){
      this.removeListener(document.body,"click",this.focusInputCard);
      this.$router.push({name:"Liquidation",params:{
        playerCardId:this.cardReader.cardId
      }});
    },
    focusInputCard(){
        document.getElementById("input-card").focus();
    },
    addListener(element,e,fn){
      element.addEventListener?element.addEventListener(e,fn,false):element.attachEvent("on" + e,fn)
    },
    removeListener(element,e,fn){
      element.removeEventListener?element.removeEventListener(e,fn,false):element.detachEvent("on" + e,fn)
    }
  },
  mounted(){
    
  }
};
</script>

<style lang="scss">
  .dashboard{
    .input-card-msg{
      font-size:30px;
      text-align:center;
      color:#4FC08D;
      margin-bottom:12px;
    }
    .input-card-wrapper{
      text-align:center;
      #input-card{
        max-width:300px;
        /*height:0;
        width:0;
        border:0;
        padding:0;
        margin:0;
        opacity: 0;*/
      }
    }
  }
</style>
