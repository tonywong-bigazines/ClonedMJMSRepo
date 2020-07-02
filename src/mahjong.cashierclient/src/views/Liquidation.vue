<template>
  <div class="liquidation">
    <el-card class="box-card">
      <div slot="header" class="clearfix">
        <span>結算 - 卡號：{{playerCardId}}</span>
      </div>
      <div class="card-body">
        <div class="liquidation-info" v-if="liquidation">
          <div class="histories-panel">
            <table class="history-table" id="print">
              <thead>
                <tr>
                  <th>臺號</th>
                  <th>回合</th>
                  <th>事件</th>
                  <th>輸贏</th>
                  <th>代打</th>
                  <th></th>
                </tr>
              </thead>
              <tbody>
                <template v-for="history in liquidation.histories">
                  <tr v-for="item in history.playHistoryDetails" :key="item.creationTime">
                    <td>{{history.tableId}}</td>
                    <td>{{history.round}}</td>
                    <td>{{item.mohjongActionName}}</td>
                    <td class="winOrLose">
                      <span :class="winOrLose(item)=='贏'?'win':'lose'">{{winOrLose(item)}}</span>
                    </td>
                    <td>{{isHelpPlay(item)}}</td>
                    <td></td>
                  </tr>
                </template>
              </tbody>
            </table>
          </div>
          <div class="sidebar">
            <table v-if="liquidation" class="table total">
              <tr>
                <td>贏:</td>
                <td>{{liquidation.win}} 場</td>
              </tr>
              <tr>
                <td>輸:</td>
                <td>{{liquidation.lose}} 場</td>
              </tr>
              <tr>
                <td>總計:</td>
                <td>${{liquidation.total}}</td>
              </tr>
            </table>
            <div class="tips">輸入電話號碼以保存麻將記錄</div>
            <el-form ref="form">
              <el-form-item>
                <el-input placeholder="電話號碼" type="text" v-model="clientPhoneNumber"></el-input>
              </el-form-item>
            </el-form>
            <div class="actions">
              <el-button type="primary" round @click="ligidation">結算</el-button>
            </div>
          </div>
        </div>
      </div>
    </el-card>

    <el-dialog
      title="結算"
      :visible.sync="printDialog.visible"
      width="30%"
      :close-on-click-modal="false"
    >
      <div id="print-area">
        <img src="http://www.bigazines.com/wp-content/uploads/2015/11/logo.png" />
        <table v-if="liquidation" class="table total">
          <tr>
            <td>電話:</td>
            <td>{{clientPhoneNumber}}</td>
          </tr>
          <tr>
            <td>入場時間:</td>
            <td>{{liquidation.checkinTime.replace("T"," ").substring(0,19)}}</td>
          </tr>
          <tr>
            <td>離座時間:</td>
            <td>{{liquidation.checkoutTime.replace("T"," ").substring(0,19)}}</td>
          </tr>
          <tr>
            <td>贏:</td>
            <td>{{liquidation.win}} 場</td>
          </tr>
          <tr>
            <td>輸:</td>
            <td>{{liquidation.lose}} 場</td>
          </tr>
          <tr>
            <td>總計:</td>
            <td>${{liquidation.total}}</td>
          </tr>
        </table>
      </div>
      <span slot="footer" class="dialog-footer">
        <el-button @click="printDialog.visible = false">關閉</el-button>
        <el-button type="primary" round v-print="'#print-area'">列印</el-button>
      </span>
    </el-dialog>
  </div>
</template>


<script>
export default {
  name: "liquidation",
  data() {
    return {
      clientPhoneNumber:"",
      liquidation: null,
      printDialog: {
        visible: false
      }
    };
  },
  methods: {
    ligidation(){
      this.axios
      .post(
        "http://mjms-core.azurewebsites.net/api/services/app/Card/Liquidation?cardId=" +
          this.playerCardId
      )
      .then(response => {
        if(response.data.success ==  true){
          this.printDialog.visible = true;
        }else{
           this.$message('結算失敗');
        }
      });
      
    },
    winOrLose(playHistoryDetail) {
      for (let i = 0; i < playHistoryDetail.players.length; i++) {
        if (playHistoryDetail.players[i].playerCardId == this.playerCardId) {
          if (playHistoryDetail.players[i].winOrLose === "Win") {
            return "贏";
          } else {
            return "輸";
          }
        }
      }
      return "";
    },
    isHelpPlay(playHistoryDetail) {
      for (let i = 0; i < playHistoryDetail.players.length; i++) {
        if (playHistoryDetail.players[i].playerCardId == this.playerCardId) {
          if (playHistoryDetail.players[i].playerType == "代打") {
            return "代打";
          }
        }
      }
      return "";
    }
  },
  mounted() {
    this.axios
      .get(
        "http://mjms-core.azurewebsites.net/api/services/app/Card/GetPlayerHistory?cardId=" +
          this.playerCardId
      )
      .then(response => {
        console.log(response.data);
        this.liquidation = response.data.result;
      });
  },
  computed: {
    playerCardId() {
      return this.$route.params.playerCardId;
    }
  }
};
</script>

<style lang="scss">
.liquidation {
  .el-card__body {
    padding: 0;
  }
  .input-card {
    font-size: 30px;
    text-align: center;
    color: #4fc08d;
  }
  .card-footer {
    text-align: right;
    border-top: 1px solid #ebeef5;
    margin-left: -20px;
    margin-right: -20px;
    padding: 15px 20px 0 20px;
  }
  .liquidation-info {
    display: flex;
    .sidebar {
      background: #eaeaea;
      width: 250px;
      padding: 20px;
    }
    .histories-panel {
      flex: 1;
    }
  }
  .actions {
    .el-button {
      width: 100%;
      margin: 0 0 10px 0;
    }
  }
  .tips {
    font-size: 14px;
    color: #888;
    margin-bottom: 8px;
  }
  .total.table {
    color: teal;
    margin-bottom: 30px;

    td {
      padding: 0 5px 5px 0;
      vertical-align: middle;

      &:first-child {
        color: #888;
        font-size: 16px;
      }

      &:last-child {
        font-weight: bold;
        font-size: 20px;
      }
    }
  }
  .history-table {
    width: 100%;
    text-align: center;
    border-collapse: collapse;
    thead {
      th {
        padding: 8px;
        background: teal;
        color: #fff;
      }
    }
    tbody tr {
      td {
        padding: 5px 5px;

        &:nth-child(1) {
          width: 80px;
        }
        &:nth-child(2) {
          width: 80px;
        }
        &:nth-child(3) {
          width: 120px;
        }
        &:nth-child(4) {
          width: 80px;
        }
        &:nth-child(5) {
          width: 80px;
        }
        &.winOrLose {
          span {
            display: inline-block;
            height: 28px;
            width: 28px;
            border-radius: 15px;
            text-align: center;
            line-height: 28px;
            font-size: 14px;
            &.win {
              background-color: teal;
              color: #fff;
            }
            &.lose {
              background-color: #888;
              color: #fff;
            }
          }
        }
      }
      &:nth-child(even) {
        td {
          background: #f2f2f2;
        }
      }
    }
  }
}
</style>